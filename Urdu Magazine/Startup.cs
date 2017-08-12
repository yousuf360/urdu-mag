using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Urdu_Magazine.Startup))]

namespace Urdu_Magazine
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
