using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FindIt.Startup))]
namespace FindIt
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
