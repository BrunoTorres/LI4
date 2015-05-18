using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AritMat.Startup))]
namespace AritMat
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
