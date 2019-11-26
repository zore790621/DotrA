using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DotrA_001_Clone.Startup))]
namespace DotrA_001_Clone
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
