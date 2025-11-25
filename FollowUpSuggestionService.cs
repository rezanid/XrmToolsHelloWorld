namespace XrmToolsHelloWorld
{
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Query;
    using System;
    using System.Linq;

    public interface IFollowUpSuggestionService
    {
        SuggestNextFollowUpApiPlugin.Response SuggestNextFollowUp(SuggestNextFollowUpApiPlugin.Request request);
    }

    public class FollowUpSuggestionService : IFollowUpSuggestionService
    {
        private readonly IOrganizationService _organizationService;
        private readonly ITracingService _tracing;

        public FollowUpSuggestionService(
            IOrganizationService organizationService,
            ITracingService tracing)
        {
            _organizationService = organizationService ?? throw new ArgumentNullException(nameof(organizationService));
            _tracing = tracing ?? throw new ArgumentNullException(nameof(tracing));
        }

        public SuggestNextFollowUpApiPlugin.Response SuggestNextFollowUp(SuggestNextFollowUpApiPlugin.Request request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.Target == null)
            {
                throw new InvalidPluginExecutionException("The Target parameter is required.");
            }

            _tracing.Trace("FollowUpSuggestionService.SuggestNextFollowUp: Start");

            var now = DateTime.UtcNow;

            // 1. Load target
            var targetRef = request.Target;

            var columns =
                targetRef.LogicalName == Contact.Meta.EntityLogicalName ?
                new ColumnSet(Contact.Meta.Fields.FullName, Contact.Meta.Fields.Telephone1, Contact.Meta.Fields.EMailAddress1) :
                targetRef.LogicalName == Account.Meta.EntityLogicalName ?
                new ColumnSet(Account.Meta.Fields.Name, Account.Meta.Fields.Telephone1, Account.Meta.Fields.EMailAddress1, Account.Meta.Fields.Revenue) :
                new ColumnSet("name");

            var target = _organizationService.Retrieve(
                targetRef.LogicalName,
                targetRef.Id,
                columns);

            // 2. Last interaction
            var lastInteraction =
                request.LastInteractionDate
                ?? TryGetLastCompletedActivityDate(targetRef)
                ?? now.AddDays(-30); // default

            var daysSinceLastInteraction = (now - lastInteraction).TotalDays;

            // 3. Suggested date
            var suggestedFollowUpDate = CalculateSuggestedFollowUpDate(now, daysSinceLastInteraction);

            // 4. Channel
            var suggestedChannel = ResolvePreferredChannel(request.PreferredChannel, target);

            // 5. Score
            var importanceScore = CalculateImportanceScore(
                daysSinceLastInteraction,
                request.UrgencyOverride,
                target);

            // 6. Reason
            var reason = BuildReason(
                target,
                lastInteraction,
                suggestedFollowUpDate,
                suggestedChannel,
                importanceScore,
                daysSinceLastInteraction,
                request.UrgencyOverride);

            _tracing.Trace("FollowUpSuggestionService.SuggestNextFollowUp: Done");

            return new SuggestNextFollowUpApiPlugin.Response
            {
                SuggestedFollowUpDate = suggestedFollowUpDate,
                SuggestedChannel = suggestedChannel,
                ImportanceScore = importanceScore,
                Reason = reason
            };
        }

        private DateTime CalculateSuggestedFollowUpDate(DateTime nowUtc, double daysSinceLastInteraction)
        {
            var today = nowUtc.Date;

            if (daysSinceLastInteraction > 60)
            {
                return today.AddDays(1);
            }

            if (daysSinceLastInteraction > 30)
            {
                return today.AddDays(3);
            }

            return today.AddDays(7);
        }

        private PreferredChannel ResolvePreferredChannel(
            PreferredChannel? requestedChannel,
            Entity target)
        {
            if (requestedChannel.HasValue)
            {
                return requestedChannel.Value;
            }

            var email = GetString(target, "emailaddress1");
            var phone = GetString(target, "telephone1") ?? GetString(target, "mobilephone");

            if (!string.IsNullOrWhiteSpace(email))
            {
                return PreferredChannel.Email;
            }

            if (!string.IsNullOrWhiteSpace(phone))
            {
                return PreferredChannel.Phone;
            }

            return PreferredChannel.Teams;
        }

        private int CalculateImportanceScore(
            double daysSinceLastInteraction,
            int? urgencyOverride,
            Entity target)
        {
            var score = 50;

            if (daysSinceLastInteraction > 60)
            {
                score += 20;
            }
            else if (daysSinceLastInteraction > 30)
            {
                score += 10;
            }

            var revenue = TryGetMoney(target, "revenue")
                          ?? TryGetMoney(target, "estimatedvalue");

            if (revenue.HasValue)
            {
                if (revenue.Value >= 100_000m)
                {
                    score += 20;
                }
                else if (revenue.Value >= 10_000m)
                {
                    score += 10;
                }
            }

            if (urgencyOverride.HasValue && urgencyOverride.Value > 0)
            {
                score += urgencyOverride.Value * 5;
            }

            if (score < 0) score = 0;
            if (score > 100) score = 100;

            return score;
        }

        private string BuildReason(
            Entity target,
            DateTime lastInteraction,
            DateTime suggestedFollowUpDate,
            PreferredChannel channel,
            int importanceScore,
            double daysSinceLastInteraction,
            int? urgencyOverride)
        {
            var displayName = GetDisplayName(target);

            var baseReason =
                $"{displayName}: last interaction was {Math.Round(daysSinceLastInteraction)} days ago on {lastInteraction:yyyy-MM-dd}.";

            var followUpPart =
                $" Suggested follow-up on {suggestedFollowUpDate:yyyy-MM-dd} via {channel}.";

            var urgencyPart = urgencyOverride.HasValue
                ? $" Urgency override {urgencyOverride.Value} applied."
                : string.Empty;

            var scorePart = $" Importance score: {importanceScore}.";

            return baseReason + followUpPart + urgencyPart + scorePart;
        }

        private string GetDisplayName(Entity entity)
        {
            if (entity == null) return "Record";

            if (entity.LogicalName == "contact")
            {
                var fullName = GetString(entity, "fullname");
                if (!string.IsNullOrWhiteSpace(fullName))
                {
                    return fullName;
                }

                var lastName = GetString(entity, "lastname");
                if (!string.IsNullOrWhiteSpace(lastName))
                {
                    return lastName;
                }
            }

            var name = GetString(entity, "name");
            if (!string.IsNullOrWhiteSpace(name))
            {
                return name;
            }

            return $"{entity.LogicalName} ({entity.Id})";
        }

        private string GetString(Entity entity, string attributeLogicalName)
        {
            if (entity == null ||
                !entity.Contains(attributeLogicalName) ||
                entity[attributeLogicalName] == null)
            {
                return null;
            }

            return entity[attributeLogicalName] as string;
        }

        private decimal? TryGetMoney(Entity entity, string attributeLogicalName)
        {
            if (entity == null ||
                !entity.Contains(attributeLogicalName) ||
                entity[attributeLogicalName] == null)
            {
                return null;
            }

            if (entity[attributeLogicalName] is Money money)
            {
                return money.Value;
            }

            return null;
        }

        private DateTime? TryGetLastCompletedActivityDate(EntityReference regarding)
        {
            if (_organizationService == null || regarding == null)
            {
                return null;
            }

            try
            {
                var query = new QueryExpression("activitypointer")
                {
                    ColumnSet = new ColumnSet("actualend"),
                    TopCount = 1
                };

                query.Criteria.AddCondition("regardingobjectid", ConditionOperator.Equal, regarding.Id);
                query.Criteria.AddCondition("statecode", ConditionOperator.Equal, 1); // Completed

                query.Orders.Add(new OrderExpression("actualend", OrderType.Descending));

                var result = _organizationService.RetrieveMultiple(query);
                var activity = result.Entities.FirstOrDefault();

                if (activity != null &&
                    activity.Contains("actualend") &&
                    activity["actualend"] is DateTime actualEnd)
                {
                    return actualEnd.ToUniversalTime();
                }
            }
            catch (Exception ex)
            {
                _tracing.Trace("TryGetLastCompletedActivityDate failed: {0}", ex);
            }

            return null;
        }
    }
}