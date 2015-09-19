using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JB2.AuthorizationServer
{
    public class Client
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ClientSecretHash { get; set; }
        public Enum.AuthGrantType AllowedGrant { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public string ReturnUrl { get; set; }
    }
}