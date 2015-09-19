using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using System.Net.Mail;

namespace JB2.AuthorizationServer.Controllers
{
    public class AccountController : Controller
    {


        public AccountController()
        {
        }

       


        public ActionResult Logintest()
        {
            LoginViewModel m = new LoginViewModel();
            return View("Test");
        }

        
    }
}