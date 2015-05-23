using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AritMat.MVC.Startup))]
namespace AritMat.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
