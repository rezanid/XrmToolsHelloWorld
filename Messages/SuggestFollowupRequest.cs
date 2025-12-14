namespace XrmToolsHelloWorld.Messages
{
    using Microsoft.Xrm.Sdk.Client;
    using System;

    [RequestProxy("rn_SuggestNextFollowUp")]
    [System.Runtime.Serialization.DataContract(Namespace = "http://schemas.microsoft.com/xrm/2011/new/")]
    internal class SuggestFollowupRequest : Microsoft.Xrm.Sdk.OrganizationRequest
    {
        public Microsoft.Xrm.Sdk.EntityReference Target { get => Parameters.TryGetValue("Target", out Microsoft.Xrm.Sdk.EntityReference value_target) ? value_target : default; set => Parameters["Target"] = value; }
        public int? UrgencyOverride { get => Parameters.TryGetValue("UrgencyOverride", out int? value_urgencyoverride) ? value_urgencyoverride : default; set => Parameters["UrgencyOverride"] = value; }
        public DateTime? LastInteractionDate { get => Parameters.TryGetValue("LastInteractionDate", out DateTime? value_lastinteractiondate) ? value_lastinteractiondate : default; set => Parameters["LastInteractionDate"] = value; }
        public Microsoft.Xrm.Sdk.OptionSetValue PreferredChannel { get => Parameters.TryGetValue("PreferredChannel", out Microsoft.Xrm.Sdk.OptionSetValue value_preferredchannel) ? value_preferredchannel : default; set => Parameters["PreferredChannel"] = value; }
    }
}
