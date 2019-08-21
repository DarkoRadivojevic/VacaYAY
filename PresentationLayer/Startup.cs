using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VacaYAY.Startup))]
namespace VacaYAY
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            
        }
    }
}
