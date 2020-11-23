using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Team6New_MIS4200.Startup))]
namespace Team6New_MIS4200
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
