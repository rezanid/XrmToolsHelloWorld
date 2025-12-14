namespace XrmToolsHelloWorld
{
    using Microsoft.Xrm.Sdk;
    using System;
    using XrmTools.Meta.Attributes;
    using XrmToolsHelloWorld.Messages;

    [Plugin]
    [Step("Create", "account", "name,description,industrycode,rn_suggestedfollowup", Stages.PreOperation, ExecutionMode.Synchronous)]
    public partial class AccountGreetingPlugin : IPlugin
    {
        readonly string _config;
        readonly string _secureConfig;
        
        public AccountGreetingPlugin(string config, string secureConfig)
        {
            _config = config;
            _secureConfig = secureConfig;
        }
        
        public string Config => _config;
        public string SecureConfig => _secureConfig;

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

        [DependencyProvider("User")]
        IOrganizationService OrganizationUserService
        {
            get => TryGet<IOrganizationService>(out var instance)
                ? instance
                : Set(ServiceFactory.CreateOrganizationService(Context.UserId));
        }

        [Dependency]
        IGreetingService GreetingService { get => Require<IGreetingService>(); }

        public void Execute(IServiceProvider serviceProvider)
        {
            using (var scope = CreateScope(serviceProvider))
            {
                Tracing.Trace("AccountGreetingPlugin: Execute started.");
                Target.Description = GreetingService.GetGreeting(Target.Name, (int?)Target.IndustryCode);

                var suggestFollowup = new SuggestFollowupRequest
                {
                    Target = Target.ToEntityReference()
                };
                var suggestion = (SuggestFollowupResponse) OrganizationService.Execute(suggestFollowup);
                Target.SuggestedFollowup = suggestion.SuggestedFollowUpDate;
            }
        }
    }
}
