using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JB2.OAuth
{
    public class Client
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ClientSecretHash { get; set; }
        public Enum.OAuthGrant AllowedGrant { get; set; }

        public DateTimeOffset CreatedOn { get; set; }
    }
}