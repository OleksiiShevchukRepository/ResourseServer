using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartup(typeof(ResourceServerOauth.Startup))]

namespace ResourceServerOauth
{
    public partial class Startup
    {
        public  void Configuration(IAppBuilder app)
        {
            //app.UseCors(CorsOptions.AllowAll);
            ConfigureOauth(app);
        }
    }
}
