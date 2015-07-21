using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace JB2.Login
{
    public static class Helper
    {
        private const string CLIENTCLAIM = "auth:client";
        private const string CLIENTSCOPE = ":scope";

        public static ClaimsIdentity CurrentAuthPlayer
        {
            get
            {
                return (ClaimsIdentity)HttpContext.Current.User.Identity;
            }
        }

        public static string CurrentAuthClientID
        {
            get
            {
                string result = string.Empty;

                try
                {
                    
                    Claim claim = Helper.CurrentAuthPlayer.Claims.FirstOrDefault(c => c.Type == CLIENTCLAIM);
                    result = claim.Value;
                }
                catch(Exception ex)
                {

                }

                return result;
            }
        }

        public static Enum.ClaimScope[] CurrentScope
        {
            get
            {
                List<Enum.ClaimScope> result = new List<Enum.ClaimScope>();

                try
                {
                    var claims = CurrentAuthPlayer.Claims;
                    var claimType = CurrentAuthClientID + CLIENTSCOPE;
                    foreach(Claim c in claims)
                    {
                        if (c.Type == claimType)
                            result.Add((Enum.ClaimScope)System.Enum.Parse(typeof(Enum.ClaimScope), c.Value));
                    }
                }
                catch(Exception ex)
                {

                }

                return result.ToArray();
            }


        }

        public static bool IsScopeAuthorized(Enum.ClaimScope scope)
        {
            try
            {
                return CurrentScope.Contains(scope);
            }
            catch
            {
                return false;
            }


        }


        


    }
}
