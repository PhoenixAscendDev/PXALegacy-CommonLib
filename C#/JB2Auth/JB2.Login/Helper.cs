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
                return (ClaimsIdentity)HttpContext.Current.User;
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
                    foreach(Claim c in CurrentAuthPlayer.Claims)
                    {
                        if (c.Type == CurrentAuthClientID + CLIENTSCOPE)
                            result.Add((Enum.ClaimScope)System.Enum.Parse(typeof(Enum.ClaimScope), c.Value));
                    }
                }
                catch(Exception ex)
                {

                }

                return result.ToArray();
            }


        }


        


    }
}
