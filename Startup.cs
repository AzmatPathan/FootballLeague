using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FootballLeague.Startup))]
namespace FootballLeague
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
