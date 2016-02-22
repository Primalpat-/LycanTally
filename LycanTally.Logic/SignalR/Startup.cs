using LycanTally.InversionOfControl;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(LycanTally.Logic.SignalR.Startup))]
namespace LycanTally.Logic.SignalR
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalHost.DependencyResolver.Register(
                typeof(IHubActivator), 
                () => new UnityHubActivator(UnityConfig.GetConfiguredContainer()));

            app.MapSignalR();
        }
    }
}