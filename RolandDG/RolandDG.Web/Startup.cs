using Microsoft.Owin;
using Owin;
using RolandDG.Web;

[assembly: OwinStartup(typeof(Startup))]
namespace RolandDG.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
