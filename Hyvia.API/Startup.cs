using System.Web.Http;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Cors;
using Swashbuckle.Application;

[assembly: OwinStartup(typeof(Hyvia.API.Startup))]

namespace Hyvia.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            //ConfigureOAuth(app);
            ConfigureAuth(app);
            HttpConfiguration config = new HttpConfiguration();
            config
                .EnableSwagger(c => c.SingleApiVersion("v1", "Hyvia Project API Documentation"))
                .EnableSwaggerUi();
            WebApiConfig.Register(config);
            //enable CORS

            app.UseWebApi(config);
        }

        /* public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }*/
    }
}
