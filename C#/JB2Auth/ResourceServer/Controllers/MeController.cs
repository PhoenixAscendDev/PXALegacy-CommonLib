using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Security.Claims;

using JB2.Login.Attributes;
using JB2.Login.Enum;


namespace ResourceServer.Controllers
{
    [Authorize]
    public class MeController : ApiController
    {
        ClaimsIdentity _identity;

        public MeController()
        {
            _identity = (ClaimsIdentity)this.User.Identity;
        }
       // +		[1]	{auth:client: 42ff5dad3c274c97a3a7c3d44b67bb42}	System.Security.Claims.Claim

        [ScopeAuthorize(ClaimScope.basic_profile)]
        [HttpGet]
        public string Get()
        {
            return JB2.Login.Helper.CurrentAuthClientID;
        }
    }
}