namespace XrmToolsHelloWorld.Messages
{
    using Microsoft.Xrm.Sdk.Client;
    using System;

    [ResponseProxy("rn_SuggestNextFollowUp")]
    [System.Runtime.Serialization.DataContract(Namespace = "http://schemas.microsoft.com/xrm/2011/new/")]
    internal class SuggestFollowupResponse : Microsoft.Xrm.Sdk.OrganizationResponse
    {
        public Microsoft.Xrm.Sdk.OptionSetValue SuggestedChannel => Results.TryGetValue("SuggestedChannel", out Microsoft.Xrm.Sdk.OptionSetValue value_suggestedchannel) ? value_suggestedchannel : default;

        public int? ImportanceScore => Results.TryGetValue("ImportanceScore", out int? value_importancescore) ? value_importancescore : default;

        public string Reason => Results.TryGetValue("Reason", out string value_reason) ? value_reason : default;

        public DateTime? SuggestedFollowUpDate => Results.TryGetValue("SuggestedFollowUpDate", out DateTime? value_suggestedfollowupdate) ? value_suggestedfollowupdate : default;
    }
}
