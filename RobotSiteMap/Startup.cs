using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RobotSiteMap.Startup))]
namespace RobotSiteMap
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
