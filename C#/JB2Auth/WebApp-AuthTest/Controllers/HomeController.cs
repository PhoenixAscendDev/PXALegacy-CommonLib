using System;
using System.Collections.Generic;
using DotNetOpenAuth.OAuth2;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Mvc;

using Constrants;

namespace WebApp_AuthTest.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult SaveToken()
        {
            var accessToken = Request.Form["htn_token"];
            Session["accessToken"] = accessToken;

            InitializeWebServerClient();
            var resourceServerUri = new Uri(Paths.ResourceServerBaseAddress);
            var client = new HttpClient(_webServerClient.CreateAuthorizingHandler(accessToken));
            var body = client.GetStringAsync(new Uri(resourceServerUri, Paths.MePath)).Result;
            
            ViewBag.ApiResponse = body;



            return View("Index");
        }

        private WebServerClient _webServerClient;
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignIn()
        {
          
            return View();
        }

        public ActionResult AGTest()
        {
            ViewBag.AccessToken = Request.Form["AccessToken"] ?? "";
            ViewBag.RefreshToken = Request.Form["RefreshToken"] ?? "";
            ViewBag.Action = "";
            ViewBag.ApiResponse = "";

            InitializeWebServerClient();
            var accessToken = Request.Form["AccessToken"];
            if (string.IsNullOrEmpty(accessToken))
            {
                var authorizationState = _webServerClient.ProcessUserAuthorization(Request);
                if (authorizationState != null)
                {
                    ViewBag.AccessToken = authorizationState.AccessToken;
                    ViewBag.RefreshToken = authorizationState.RefreshToken;
                    ViewBag.Action = Request.Path;
                }
            }

            if (!string.IsNullOrEmpty(Request.Form.Get("submit.Authorize")))
            {
                var userAuthorization = _webServerClient.PrepareRequestUserAuthorization(new[] { "basic_profile","birthday","public_game_activity","testDenied" });
                userAuthorization.Send(HttpContext);
                Response.End();
            }
            else if (!string.IsNullOrEmpty(Request.Form.Get("submit.Refresh")))
            {
                var state = new AuthorizationState
                {
                    AccessToken = Request.Form["AccessToken"],
                    RefreshToken = Request.Form["RefreshToken"]
                };
                if (_webServerClient.RefreshAuthorization(state))
                {
                    ViewBag.AccessToken = state.AccessToken;
                    ViewBag.RefreshToken = state.RefreshToken;
                }
            }
            else if (!string.IsNullOrEmpty(Request.Form.Get("submit.CallApi")))
            {
                var resourceServerUri = new Uri(Paths.ResourceServerBaseAddress);
                var client = new HttpClient(_webServerClient.CreateAuthorizingHandler(accessToken));
                var body = client.GetStringAsync(new Uri(resourceServerUri, Paths.MePath)).Result;
                ViewBag.ApiResponse = body;
            }

            return View();
        }

        private void InitializeWebServerClient()
        {
            var authorizationServerUri = new Uri(Paths.AuthorizationServerBaseAddress);
            var authorizationServer = new AuthorizationServerDescription
            {
                AuthorizationEndpoint = new Uri(authorizationServerUri, Paths.AuthorizePath),
                TokenEndpoint = new Uri(authorizationServerUri, Paths.TokenPath)
            };
            _webServerClient = new WebServerClient(authorizationServer, Clients.Client2.Id, Clients.Client2.Secret);
        }
    }
}