using Microsoft.Xrm.Sdk;
using System;
using XrmTools.Meta.Attributes;

namespace XrmToolsHelloWorld
{
    [Plugin]
    [Step("Create", "account", "name, description, industrycode", Stages.PreOperation, ExecutionMode.Synchronous)]
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

        [Dependency]
        IGreetingService GreetingService { get => Require<IGreetingService>(); }

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

        public void Execute(IServiceProvider serviceProvider)
        {
            using (var scope = CreateScope(serviceProvider))
            {
                Tracing.Trace("AccountGreetingPlugin: Execute started.");
                Target.Description = GreetingService.GetGreeting(Target.Name, (int?)Target.IndustryCode);
            }
        }
    }
}
