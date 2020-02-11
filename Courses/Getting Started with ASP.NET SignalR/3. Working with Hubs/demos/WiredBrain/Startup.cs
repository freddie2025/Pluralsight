using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using WiredBrain.Connections;

[assembly: OwinStartup(typeof(WiredBrain.Startup))]

namespace WiredBrain
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            //app.MapSignalR<CoffeeConnection>("/coffee");
            //GlobalHost.HubPipeline.RequireAuthentication();
        }
    }
}
