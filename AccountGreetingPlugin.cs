using Microsoft.Xrm.Sdk;
using System;
using XrmTools.Meta.Attributes;

namespace XrmToolsHelloWorld
{
    [Plugin]
    [Step("Create", "account", Stages.PreOperation, ExecutionMode.Synchronous)]
    public partial class AccountGreetingPlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            var target = (Entity)context.InputParameters["Target"];
            target["description"] = "Hello from XrmToolsHelloWorld Plugin!"];
        }
    }
}
