using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Extensions;
using Microsoft.Xrm.Sdk.PluginTelemetry;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using XrmTools;

namespace XrmToolsHelloWorld
{
    [GeneratedCode("TemplatedCodeGenerator", "1.5.0.3")]
    public partial class SuggestNextFollowUpApiPlugin
    {
        /// <summary>
        /// This method should be called before accessing any target, image or any of your dependencies.
        /// </summary>
        protected IDisposable CreateScope(IServiceProvider serviceProvider)
        {
            var scope = new DependencyScope<SuggestNextFollowUpApiPlugin>();
            scope.Set<IServiceProvider>(serviceProvider);
        
            var iTracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
        
            scope.Set<IPluginExecutionContext>((IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext)));
            scope.Set<IOrganizationServiceFactory>((IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory)));
            scope.Set<ITracingService>((ITracingService)serviceProvider.GetService(typeof(ITracingService)));
            scope.Set<XrmToolsHelloWorld.IFollowUpSuggestionService>(scope.Set(new XrmToolsHelloWorld.FollowUpSuggestionService(this.OrganizationService, (ITracingService)serviceProvider.GetService(typeof(ITracingService)))));
            return scope;
        }
	    private static T EntityOrDefault<T>(DataCollection<string, object> keyValues, string key) where T : Entity
        {
            if (keyValues is null) return default;
            return keyValues.TryGetValue(key, out var obj) ? obj is Entity entity ? entity.ToEntity<T>() : default : default;
        }

        private static T EntityOrDefault<T>(DataCollection<string, Entity> keyValues, string key) where T : Entity
        {
            if (keyValues is null) return default;
            return keyValues.TryGetValue(key, out var entity) ? entity?.ToEntity<T>() : default;
        }

        private static T Require<T>() => DependencyScope<SuggestNextFollowUpApiPlugin>.Current.Require<T>();
        private static T Require<T>(string name) => DependencyScope<SuggestNextFollowUpApiPlugin>.Current.Require<T>(name);

        private static bool TryGet<T>(out T instance) => DependencyScope<SuggestNextFollowUpApiPlugin>.Current.TryGet(out instance);
        private static bool TryGet<T>(string name, out T instance) => DependencyScope<SuggestNextFollowUpApiPlugin>.Current.TryGet(name, out instance);

        private static T Set<T>(T instance) => DependencyScope<SuggestNextFollowUpApiPlugin>.Current.Set(instance);
        private static T Set<T>(string name, T instance) => DependencyScope<SuggestNextFollowUpApiPlugin>.Current.Set(name, instance);
        private static T SetAndTrack<T>(T instance) where T : IDisposable => DependencyScope<SuggestNextFollowUpApiPlugin>.Current.SetAndTrack(instance);
        private static T SetAndTrack<T>(string name, T instance) where T : IDisposable => DependencyScope<SuggestNextFollowUpApiPlugin>.Current.SetAndTrack(name, instance);

        private static SuggestNextFollowUpApiPlugin.Request GetRequest(IExecutionContext context)
        {
            var request = new SuggestNextFollowUpApiPlugin.Request();
            request.Target = context.InputParameters.TryGetValue("Target", out Microsoft.Xrm.Sdk.EntityReference target) ? target : default;
            request.PreferredChannel = context.InputParameters.TryGetValue("PreferredChannel", out OptionSetValue preferredchannel) ? (XrmToolsHelloWorld.PreferredChannel?)preferredchannel.Value : default;
            request.LastInteractionDate = context.InputParameters.TryGetValue("LastInteractionDate", out System.DateTime? lastinteractiondate) ? lastinteractiondate : default;
            request.UrgencyOverride = context.InputParameters.TryGetValue("UrgencyOverride", out int? urgencyoverride) ? urgencyoverride : default;
            return request;
        }

        private static void SetResponse(IExecutionContext context, SuggestNextFollowUpApiPlugin.Response response)
        {
            if (response.SuggestedFollowUpDate is DateTime suggestedfollowupdateValue) context.OutputParameters["SuggestedFollowUpDate"] = suggestedfollowupdateValue;
            if (response.SuggestedChannel is XrmToolsHelloWorld.PreferredChannel suggestedchannelValue) context.OutputParameters["SuggestedChannel"] = new OptionSetValue((int)suggestedchannelValue);
            if (response.ImportanceScore is int importancescoreValue) context.OutputParameters["ImportanceScore"] = importancescoreValue;
            if (response.Reason is string reasonValue) context.OutputParameters["Reason"] = reasonValue;
        }
    }
}
