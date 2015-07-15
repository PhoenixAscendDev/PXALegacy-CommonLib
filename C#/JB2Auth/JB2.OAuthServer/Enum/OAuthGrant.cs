using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JB2.OAuth.Enum
{
    public enum OAuthGrant
    {
        Code = 1,
        Implicit = 2,
        ResourceOwner = 3,
        Client = 4
        
    }
}