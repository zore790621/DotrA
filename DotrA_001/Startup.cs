using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DotrA_001.Startup))]
namespace DotrA_001
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
