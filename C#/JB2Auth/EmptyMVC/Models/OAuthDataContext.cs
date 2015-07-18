using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;



namespace JB2.AuthorizationServer
{
    public class OAuthDataContext: IdentityDbContext
    {

        public OAuthDataContext()
            : base("OAuthDbContext")
        {

        }

        public DbSet<Client> Clients { get; set; }
    }

}