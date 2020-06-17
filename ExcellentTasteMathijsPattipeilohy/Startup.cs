using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ExcellentTasteMathijsPattipeilohy.Startup))]
namespace ExcellentTasteMathijsPattipeilohy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
