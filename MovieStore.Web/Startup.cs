using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MovieStore.Web.Startup))]
namespace MovieStore.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
