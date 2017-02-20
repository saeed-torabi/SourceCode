using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Board.Web.Startup))]
namespace Board.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
