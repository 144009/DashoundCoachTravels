using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DashoundCoachTravels.Startup))]
namespace DashoundCoachTravels
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
