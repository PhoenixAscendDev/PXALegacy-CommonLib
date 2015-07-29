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
        private const string USERIDCLAIM = ":userid";

        public static ClaimsIdentity CurrentAuthPlayer
        {
            get
            {
                return (ClaimsIdentity)HttpContext.Current.User.Identity;
            }
        }

        public static JB2.Login.Player  RetrieveProfile(string playerID)
        {
            JB2.Login.Data.LoginRepository repo = new Data.LoginRepository();
            return repo.GetPlayer(playerID);
        }

        public static string CurrentAuthUserId
        {
            get
            {
                string result = string.Empty;
                try
                {

                    Claim claim = Helper.CurrentAuthPlayer.Claims.FirstOrDefault(c => c.Type == CurrentAuthClientID + USERIDCLAIM);
                    result = claim.Value;
                }
                catch(Exception ex)
                {

                }
                return result;

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


        public static Enum.ClaimScope[] RetreievePlayerScope(string playerID,string clientID )
        {

            JB2.Login.Data.LoginRepository repo = new Data.LoginRepository();
            try
            {
                return repo.GetPlayerClient(playerID, clientID).Scope;
            }
            catch(Exception ex)
            {
                return new Enum.ClaimScope[0];
            }
            
         
        }


        public static bool IsScopeAuthorized(Enum.ClaimScope[]  scopes,Enum.ClaimScope test)
        {
            return scopes.Contains(test);
        }

        public static bool IsScopeAuthorized(Enum.ClaimScope[] scopes, string test)
        {
            try
            {
                Enum.ClaimScope scope = (Enum.ClaimScope)System.Enum.Parse(typeof(Enum.ClaimScope), test);
                return IsScopeAuthorized(scopes, scope);
            }
            catch
            {
                return false;
            }


        }




        


    }
}
