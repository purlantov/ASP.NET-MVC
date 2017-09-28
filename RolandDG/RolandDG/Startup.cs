using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Roland_ASP_MVC.Startup))]
namespace Roland_ASP_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
