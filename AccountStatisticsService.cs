namespace XrmToolsHelloWorld
{
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Query;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security;
    using XrmTools.Meta.Attributes;

    public interface IAccountStatisticsService
    {
        int GetAccountCount(IEnumerable<ConditionExpression> conditions = null);
    }

    public class AccountStatisticsService : IAccountStatisticsService
    {
        private readonly IOrganizationService _service;
        
        public AccountStatisticsService()
        {
        }

        [DependencyConstructor]
        public AccountStatisticsService(
            string config,
            string secureConfig,
            IOrganizationService service)
        {
            // Currently we don't use config or secureConfig, but they are available for future use.

            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
        
        public int GetAccountCount(IEnumerable<ConditionExpression> conditions = null)
        {
            if (_service == null) throw new ArgumentNullException(nameof(_service));

            var filterXml = BuildFilterXml(conditions);

            // Aggregate FetchXML to count accounts
            var fetchXml = $@"
            <fetch distinct='false' aggregate='true'>
              <entity name='account'>
                <attribute name='accountid' aggregate='count' alias='account_count' />
                {filterXml}
              </entity>
            </fetch>";

            var result = _service.RetrieveMultiple(new FetchExpression(fetchXml));

            if (result.Entities.Count == 0)
                return 0;

            var aliased = result.Entities[0].GetAttributeValue<AliasedValue>("account_count");
            var value = aliased?.Value;

            // The provider may return Int32, Int64, Decimal, or Double depending on backend.
            return Convert.ToInt32(value);
        }

        private static string BuildFilterXml(IEnumerable<ConditionExpression> conditions)
        {
            if (conditions == null) return string.Empty;

            var conds = conditions.ToList();
            if (conds.Count == 0) return string.Empty;

            var parts = conds.Select(c => ToConditionXml(c));
            return $"<filter type='and'>{string.Concat(parts)}</filter>";
        }

        private static string ToConditionXml(ConditionExpression c)
        {
            // Only supports simple (attribute operator value) conditions.
            // Extend here for multi-values (e.g., In) or two-value ops (Between) as needed.
            var attribute = XmlEscape(c.AttributeName);
            var op = c.Operator.ToString().ToLowerInvariant(); // maps most operators correctly (eq, ne, gt, etc.)
            if (c.Values == null || c.Values.Count == 0)
            {
                // For operators that don't need a value (e.g., null/not-null), Dataverse uses operators "null" / "not-null"
                // ConditionExpression usually uses Equal/NotEqual with null, but just in case:
                return $"<condition attribute='{attribute}' operator='{op}' />";
            }

            // Single value condition
            var value = XmlEscape(ValueToString(c.Values[0]));
            return $"<condition attribute='{attribute}' operator='{op}' value='{value}' />";
        }

        private static string XmlEscape(string s) => SecurityElement.Escape(s);

        private static string ValueToString(object value)
        {
            if (value == null) return string.Empty;

            // Convert typical types to string for FetchXML values.
            // Guids, ints, decimals, bools, strings, OptionSetValue, DateTime, etc.
            switch (value)
            {
                case OptionSetValue osv: return osv.Value.ToString();
                case DateTime dt: return dt.ToString("o"); // ISO 8601
                default: return value.ToString();
            }
        }
    }
}
