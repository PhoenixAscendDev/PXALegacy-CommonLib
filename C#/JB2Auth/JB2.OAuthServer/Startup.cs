using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

using JB2.OAuth;
using JB2.OAuth.Enum;

[assembly: OwinStartup(typeof(JB2.OAuth.Startup))]
namespace JB2.OAuth
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<OAuthDbContext>(() => new OAuthDbContext());
            app.CreatePerOwinContext<UserManager<IdentityUser>>(CreateManager);

            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/oauth/token"),
                Provider = new JB2OAuthAuthorizationServerProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
#if DEBUG
                AllowInsecureHttp = true,
#endif
            });
        }

        private static UserManager<IdentityUser> CreateManager(
            IdentityFactoryOptions<UserManager<IdentityUser>> options,
            IOwinContext context)
        {
            var userStore =
                new UserStore<IdentityUser>(context.Get<OAuthDbContext>());

            var manager =
                new UserManager<IdentityUser>(userStore);

            return manager;
        }



    }
}