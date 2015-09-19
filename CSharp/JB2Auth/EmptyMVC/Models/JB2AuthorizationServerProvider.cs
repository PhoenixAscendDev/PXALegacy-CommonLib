using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JB2.AuthorizationServer
{
    public class JB2AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
      

        public JB2AuthorizationServerProvider()
        {
            
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            string clientSecret;

            if (context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                UserManager<IdentityUser> userManager =
                    context.OwinContext.GetUserManager<UserManager<IdentityUser>>();
                OAuthDataContext dbContext = context.OwinContext.Get<OAuthDataContext>();

                try
                {
                    Client client = await dbContext
                        .Clients
                        .FirstOrDefaultAsync(clientEntity => clientEntity.Id == clientId);

                    if (client != null &&
                        userManager.PasswordHasher.VerifyHashedPassword(
                            client.ClientSecretHash, clientSecret) == PasswordVerificationResult.Success)
                    {
                        // Client has been verified.
                        context.OwinContext.Set<Client>("oauth:client", client);
                        context.Validated(clientId);
                    }
                    else
                    {
                        // Client could not be validated.
                        context.SetError("invalid_client", "Client credentials are invalid.");
                        context.Rejected();
                    }
                }
                catch
                {
                    // Could not get the client through the IClientManager implementation.
                    context.SetError("server_error");
                    context.Rejected();
                }
            }
            else
            {
                // The client credentials could not be retrieved.
                context.SetError(
                    "invalid_client",
                    "Client credentials could not be retrieved through the Authorization header.");

                context.Rejected();
            }
        }

        public override async Task GrantResourceOwnerCredentials(
    OAuthGrantResourceOwnerCredentialsContext context)
        {
            Client client = context.OwinContext.Get<Client>("oauth:client");
            if (client.AllowedGrant == Enum.AuthGrantType.ResourceOwner)
            {
                // Client flow matches the requested flow. Continue...
                UserManager<IdentityUser> userManager =
                    context.OwinContext.GetUserManager<UserManager<IdentityUser>>();

                IdentityUser user;
                try
                {
                    user = await userManager.FindAsync(context.UserName, context.Password);
                }
                catch
                {
                    // Could not retrieve the user.
                    context.SetError("server_error");
                    context.Rejected();

                    // Return here so that we don't process further. Not ideal but needed to be done here.
                    return;
                }

                if (user != null)
                {
                    try
                    {
                        // User is found. Signal this by calling context.Validated
                        ClaimsIdentity identity = await userManager.CreateIdentityAsync(
                            user,
                            DefaultAuthenticationTypes.ExternalBearer);

                        context.Validated(identity);
                    }
                    catch
                    {
                        // The ClaimsIdentity could not be created by the UserManager.
                        context.SetError("server_error");
                        context.Rejected();
                    }
                }
                else
                {
                    // The resource owner credentials are invalid or resource owner does not exist.
                    context.SetError(
                        "access_denied",
                        "The resource owner credentials are invalid or resource owner does not exist.");

                    context.Rejected();
                }
            }
            else
            {
                // Client is not allowed for the 'Resource Owner Password Credentials Grant'.
                context.SetError(
                    "invalid_grant",
                    "Client is not allowed for the 'Resource Owner Password Credentials Grant'");

                context.Rejected();
            }
        }

        public override async Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {

            string clientId = context.ClientId;
            string clientSecret;

            //if (context.TryGetBasicCredentials(out clientId, out clientSecret))
            //{



            UserManager<IdentityUser> userManager = context.OwinContext.GetUserManager<UserManager<IdentityUser>>();

            OAuthDataContext dbContext = context.OwinContext.Get<OAuthDataContext>();
            
           

            try
            {
                Client client = await dbContext.Clients.FirstOrDefaultAsync(clientEntity => clientEntity.Id == clientId);

                context.Validated(client.ReturnUrl);
                
                //Client client = dbContext.
                        //.Clients
                        //.FirstOrDefaultAsync(clientEntity => clientEntity.Id == clientId);


            }
            catch(Exception ex)
            {

            }






            //if (context.ClientId == Clients.Client1.Id)
            //{
            //    context.Validated(Clients.Client1.RedirectUrl);
            //}
            //else if (context.ClientId == Clients.Client2.Id)
            //{
            //    context.Validated(Clients.Client2.RedirectUrl);
            //}
            //return Task.FromResult(0);
        }
    }
}