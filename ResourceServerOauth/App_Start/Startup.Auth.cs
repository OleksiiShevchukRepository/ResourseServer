using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using MySolution.Core.Entities.Mongo;
using MySolution.Services.Interfaces;
using Ninject;
using Owin;

namespace ResourceServerOauth
{
	public partial class Startup
	{
        public static OAuthBearerAuthenticationOptions OAuthOptions { get; private set; }

        public void ConfigureOauth(IAppBuilder app)
        {
            app.UseOAuthBearerAuthentication(new Microsoft.Owin.Security.OAuth.OAuthBearerAuthenticationOptions());
        }
    }

    public class AuthorizationProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var client = NinjectWebCommon.Bootstrapper.Kernel.Get<IClientService>();

            var currentClient = client.GetByCredentials(context.UserName, context.Password);
            if (currentClient == null)
            {
                context.SetError("Username or password incorrect");
                return Task.FromResult<object>(null);
            }
            // var refreshTokenService = NinjectWebCommon.Bootstrapper.Kernel.Get<IRefreshTokenService>();
            // var refreshToken = refreshTokenService.GetByClientId(currentClient.Id);

            var identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, currentClient.Id.ToString()));

            var properties = new AuthenticationProperties(GetAuthenticationProperties(currentClient));
            var ticket = new AuthenticationTicket(identity, properties);

            context.Validated(ticket);

            return Task.FromResult<object>(null);
        }

        private Dictionary<string, string> GetAuthenticationProperties(Client client)
        {
            var result = new Dictionary<string, string>
            {
                { "ClientId", client.Id.ToString() },
                { "ClientName", client.FirstName + client.LastName },
            };

            return result;
        }
    }
}