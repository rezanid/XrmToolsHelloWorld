using Microsoft.Xrm.Sdk;
using System;

namespace XrmToolsHelloWorld
{
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
