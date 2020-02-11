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
            app.MapAzureSignalR("Coffee");

            //var redisConnectionString = "pluralsight.redis.cache.windows.net:6380,password=[password],ssl=True,abortConnect=False";
            //GlobalHost.DependencyResolver.UseStackExchangeRedis(
            //    new RedisScaleoutConfiguration(redisConnectionString, "Coffee"));
        }
    }
}