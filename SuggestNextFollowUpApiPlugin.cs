namespace XrmToolsHelloWorld
{
    using Microsoft.Xrm.Sdk;
    using System;
    using XrmTools.Meta.Attributes;

    [Plugin]
    [CustomApi("rn_SuggestNextFollowUp", Name = "SuggestNextFollowUp", DisplayName = "Suggest Next Follow Up", Description = "Suggests the next follow-up action for a given entity.", StepType = ProcessingStepTypes.SyncAndAsync)]
    public partial class SuggestNextFollowUpApiPlugin : IPlugin
    {
        [Dependency]
        IPluginExecutionContext Context { get => Require<IPluginExecutionContext>(); }

        [Dependency]
        IOrganizationServiceFactory ServiceFactory { get => Require<IOrganizationServiceFactory>(); }

        [Dependency]
        ITracingService Tracing { get => Require<ITracingService>(); }

        [DependencyProvider]
        IOrganizationService OrganizationService
        {
            get => TryGet<IOrganizationService>(out var instance)
                ? instance
                : Set(ServiceFactory.CreateOrganizationService(null));
        }

        [Dependency]
        IFollowUpSuggestionService FollowUpSuggestionService => Require<IFollowUpSuggestionService>();
        
        public void Execute(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null) throw new InvalidPluginExecutionException("missing " + nameof(serviceProvider));

            using (var scope = CreateScope(serviceProvider))
            {
                var request = GetRequest(Context);

                if (request.Target is null) throw new InvalidPluginExecutionException("You forgot the Target!", PluginHttpStatusCode.BadRequest);

                var response = FollowUpSuggestionService.SuggestNextFollowUp(request);

                SetResponse(Context, response);
            }
        }

        [CustomApiRequest]
        public class Request
        {
            public EntityReference Target { get; set; }
            public int? UrgencyOverride { get; set; }
            public DateTime? LastInteractionDate { get; set; }
            public CommunicationChannel? PreferredChannel { get; set; }
        }

        [CustomApiResponse]
        public class Response
        {
            public CommunicationChannel SuggestedChannel { get; set; }
            public int ImportanceScore { get; set; }
            public string Reason { get; set; }
            public DateTime SuggestedFollowUpDate { get; set; }
        }
    }
    
    public enum CommunicationChannel
    {
        Phone, Email, Teams, InPerson
    }
}