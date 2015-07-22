using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace JB2.AuthorizationServer.Controllers
{
    public class OAuthController : Controller
    {
        public ActionResult Authorize()
        {
            if (Response.StatusCode != 200)
            {
                return View("AuthorizeError");
            }

            var authentication = HttpContext.GetOwinContext().Authentication;
            var ticket = authentication.AuthenticateAsync("Application").Result;
            var identity = ticket != null ? ticket.Identity : null;
            if (identity == null)
            {
                authentication.Challenge("Application");
                return new HttpUnauthorizedResult();
            }

            var clientid = Request.QueryString.Get("client_id");
            var scopes = (Request.QueryString.Get("scope") ?? "").Split(',');


            //If user already has granted the scope then go ahead and sign them in 

            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.FindByName(identity.Name);

            JB2.Login.Enum.ClaimScope[] dbScopes = JB2.Login.Helper.RetreievePlayerScope(user.Id, clientid);
            bool needGrant = false;
            if(dbScopes.Length > 0)
            {
                foreach(string s in scopes)
                {
                    if (!JB2.Login.Helper.IsScopeAuthorized(dbScopes, s))
                        needGrant = true;
                }
            }
            else
            {
                needGrant = true;
            }

            if(!needGrant)
            {
                //sign the user in,already granted previously
               
                
                identity = new ClaimsIdentity(identity.Claims, "Bearer", identity.NameClaimType, identity.RoleClaimType);

                //add userid - right now global id but would like user to have a unquie id per client for security
                Claim idClaim = new Claim(clientid + ":userid", user.Id);
                identity.AddClaim(idClaim);
                AddClaim(identity.Name, idClaim);

                // add the clientid to identity so API can test against the scope
                identity.AddClaim(new Claim("auth:client", clientid));
                foreach (var scope in scopes)
                {
                    Claim scopeClaim = new Claim(clientid + ":scope", scope);

                    identity.AddClaim(scopeClaim);
                    AddClaim(identity.Name, scopeClaim);
                    //if (!userManager.GetClaims("aeffcd94-0804-450f-9caf-265c3b54009d").Contains(scopeClaim))
                    //   userManager.AddClaim("aeffcd94-0804-450f-9caf-265c3b54009d", scopeClaim);
                }
                authentication.SignIn(identity);
            }



            if (Request.HttpMethod == "POST")
            {
                if (!string.IsNullOrEmpty(Request.Form.Get("submit.Grant")))
                {
                    //var dbClaims = userManager.GetClaims("aeffcd94-0804-450f-9caf-265c3b54009d");
                    identity = new ClaimsIdentity(identity.Claims, "Bearer", identity.NameClaimType, identity.RoleClaimType);

                    // add the clientid to identity so API can test against the scope
                    identity.AddClaim(new Claim("auth:client", clientid));
                    foreach (var scope in scopes)
                    {
                        Claim scopeClaim = new Claim(clientid + ":scope", scope);
                        
                        identity.AddClaim(scopeClaim);
                        AddClaim(identity.Name, scopeClaim);
                        //if (!userManager.GetClaims("aeffcd94-0804-450f-9caf-265c3b54009d").Contains(scopeClaim))
                         //   userManager.AddClaim("aeffcd94-0804-450f-9caf-265c3b54009d", scopeClaim);
                    }
                    authentication.SignIn(identity);
                }
                if (!string.IsNullOrEmpty(Request.Form.Get("submit.Login")))
                {
                    authentication.SignOut("Application");
                    authentication.Challenge("Application");
                    return new HttpUnauthorizedResult();
                }
            }

            return View();
        }


        private bool AddClaim(string username, Claim newClaim)
        {

            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.FindByName(username);

            if(user != null)
            {
                var dbClaims = userManager.GetClaims(user.Id);
                bool existsAlready = false;
                foreach(Claim c in dbClaims)
                {
                    if( (c.Type == newClaim.Type) && (c.Value == newClaim.Value))
                        existsAlready = true;
                }

                if(!existsAlready)
                    userManager.AddClaim(user.Id,newClaim);
            }
            return true;

        }
    }
}