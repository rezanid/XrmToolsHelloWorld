using Microsoft.Xrm.Sdk;
using System;
using XrmTools.Meta.Attributes;

namespace XrmToolsHelloWorld
{
    [Plugin]
    [Step("Create", "account", "name, description", Stages.PreOperation, ExecutionMode.Synchronous)]
    public partial class AccountGreetingPlugin : IPlugin
    {
        [Dependency]
        ITracingService Tracing { get => Require<ITracingService>(); }

        public void Execute(IServiceProvider serviceProvider)
        {
            using (var scope = CreateScope(serviceProvider))
            {
                Tracing.Trace("AccountGreetingPlugin: Execute started.");
                Target.Description = $"Welcome {Target.Name}";
            }
        }
    }
}
