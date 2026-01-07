using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace XrmToolsHelloWorld.Queries
{
    internal static partial class FetchQueries
    {
        public static EntityCollection QueryAccountStatistics(
            this IOrganizationService service,
            string filterXml = @"
		  <filter type=""and"">
			  <condition attribute=""name"" operator=""like"" value=""a%"" />
		  </filter>")
        {
            var fetchXml = $@"
<fetch distinct=""false"" aggregate=""true"" >
  <entity name=""account"">
	  <attribute name=""accountid"" aggregate=""count"" alias=""account_count""/>{filterXml}
  </entity>
</fetch>";
            return service.RetrieveMultiple(new FetchExpression(fetchXml));
        }
    }
}