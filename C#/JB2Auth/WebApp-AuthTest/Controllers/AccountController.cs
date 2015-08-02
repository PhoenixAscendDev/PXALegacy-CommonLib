using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp_AuthTest.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public string SignIn(string accesstoken, string userid)
        {
            Session["AccessToken"] = accesstoken;
        }
    }
}