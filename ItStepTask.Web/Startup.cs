using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ItStepTask.Web.Startup))]
namespace ItStepTask.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
