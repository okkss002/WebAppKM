using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebAppKM.Startup))]
namespace WebAppKM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
