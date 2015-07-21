using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JB2.Login.Enum
{
    public enum AuthGrantType
    {
        Code = 1,
        Implicit = 2,
        ResourceOwner = 3,
        Client = 4        
    }
}