using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web;
using System.Web.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;

namespace JB2.Login.Attributes
{
    public class ScopeAuthorizeAttribute : AuthorizeAttribute
    {
        private Enum.ClaimScope _scope;

        public ScopeAuthorizeAttribute(Enum.ClaimScope scope)
        {
            this._scope = scope;
        }

        public override void OnAuthorization(HttpActionContext filterContext)
        {
            var user = JB2.Login.Helper.CurrentAuthPlayer;

            if (JB2.Login.Helper.IsScopeAuthorized(this._scope))
                base.OnAuthorization(filterContext);           
            else
                base.HandleUnauthorizedRequest(filterContext);
        }
    }


}
