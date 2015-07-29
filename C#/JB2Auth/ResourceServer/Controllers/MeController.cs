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
        public ProfileViewModel Get()
        {
            string playerID = JB2.Login.Helper.CurrentAuthClientID;
            

            //JB2.Login.Player player = JB2.Login.Player

            ProfileViewModel result = new ProfileViewModel();

            try
            {
                JB2.Login.Player player = JB2.Login.Helper.RetrieveProfile(playerID);
                result.ID = player.ID;            
                result.Displayname = player.DisplayName;
                result.ProfileUrl = JB2.Gravatar.GetImageUrl(player.Email, 100, string.Empty);
                result.jBeanWallet = new JB2.Bowtie.Economy.JBeanWallet();
                
                result.Age = 35;

                if (JB2.Login.Helper.IsScopeAuthorized(ClaimScope.birthday))
                    result.Birthday = player.Birthdate;
                if (JB2.Login.Helper.IsScopeAuthorized(ClaimScope.email))
                    result.Email = player.Email;
            }
            catch(Exception ex)
            {
                ///TODO
            }



            return new ProfileViewModel();

           
        }
    }
}