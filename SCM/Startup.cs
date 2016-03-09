using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SCM.Startup))]
namespace SCM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
