using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace JB2.AuthorizationServer
{
    public class ApplicationDataContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDataContext()
            : base("OAuthDbContext", throwIfV1Schema: false)
        {
        }

        public static ApplicationDataContext Create()
        {
            return new ApplicationDataContext();
        }
    }
}