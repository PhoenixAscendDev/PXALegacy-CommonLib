using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace JB2.OAuth
{
    public class OAuthDbContext : IdentityDbContext
    {

        public OAuthDbContext()
            : base("OAuthDbContext")
        {

        }

        public DbSet<Client> Clients { get; set; }
    }
}