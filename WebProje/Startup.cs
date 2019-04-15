using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebProje.Startup))]
namespace WebProje
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
