using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WatanART.Startup))]
namespace WatanART
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
