using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LuxeLegacy.Startup))]
namespace LuxeLegacy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
