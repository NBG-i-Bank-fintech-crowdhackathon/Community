using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(nbgService.Startup))]

namespace nbgService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
            app.MapSignalR();
        }
    }
}