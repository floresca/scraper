using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ScraperFront.Startup))]
namespace ScraperFront
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
