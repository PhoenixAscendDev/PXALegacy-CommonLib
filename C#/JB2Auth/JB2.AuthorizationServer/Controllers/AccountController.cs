using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Data.Entity.Validation;
using System.Net.Mail;

namespace JB2.AuthorizationServer.Controllers
{
    public class AccountController : Controller
    {

        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set { _signInManager = value; }
        }


        
        public ActionResult Login(LoginViewModel﻿ model)
        {
            var authentication = HttpContext.GetOwinContext().Authentication;

            if (Request.HttpMethod == "POST")
            {
                var isPersistent = !string.IsNullOrEmpty(Request.Form.Get("isPersistent"));

                if (!string.IsNullOrEmpty(Request.Form.Get("submit.Signin")))
                {
                    string username = model.Email; // Request.Form["username"];
                    string password = model.Password;

                    var user = UserManager.Find(username, password);

                    if(user != null)
                    {
                        authentication.SignIn(
                                        new AuthenticationProperties { IsPersistent = isPersistent },
                                        new ClaimsIdentity(new[] { new Claim(
                                                ClaimsIdentity.DefaultNameClaimType, username) },"Application"));
                    }
                }
            }

            LoginViewModel m = new LoginViewModel();

            ViewBag.FormMode = "login";
            ViewBag.Jscript = "alert('login');";
            return View();
        }

        public ActionResult Logout()
        {
            var authentication = HttpContext.GetOwinContext().Authentication;
            authentication.SignOut();

            ViewBag.FormMode = "login";
            ViewBag.Jscript = "alert('login');";

            return View("Login");
        }

        public ActionResult External()
        {
            var authentication = HttpContext.GetOwinContext().Authentication;
            if (Request.HttpMethod == "POST")
            {
                foreach (var key in Request.Form.AllKeys)
                {
                    if (key.StartsWith("submit.External.") && !string.IsNullOrEmpty(Request.Form.Get(key)))
                    {
                        var authType = key.Substring("submit.External.".Length);
                        authentication.Challenge(authType);
                        return new HttpUnauthorizedResult();
                    }
                }
            }
            var identity = authentication.AuthenticateAsync("External").Result.Identity;
            if (identity != null)
            {
                authentication.SignOut("External");
                authentication.SignIn(
                    new AuthenticationProperties { IsPersistent = true },
                    new ClaimsIdentity(identity.Claims, "Application", identity.NameClaimType, identity.RoleClaimType));
                return Redirect(Request.QueryString["ReturnUrl"]);
            }

            return View();
        }


        #region User

        // POST: /Account/Register
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(LoginViewModel﻿ model)
        {
            string username = model.RegisterUser.Email;
            string password = model.RegisterUser.Password;
            DateTime birthday = model.RegisterUser.Birthdate;
            string displayname = model.RegisterUser.DisplayName;
            ViewBag.FormMode = "register";
            JB2.Common.ServiceResult serviceResult = true;
            try
            {
                if (UserManager.FindById(username) != null)
                    serviceResult.Validation.Add(new JB2.Common.Validation("email", "The Email already exists") { IsValid = false });

                if (birthday == DateTime.MinValue)
                    serviceResult.Validation.Add(new JB2.Common.Validation("birthday", "Issue with Birthday") { IsValid = false });

                var passwordCheck = await UserManager.PasswordValidator.ValidateAsync(password);
                if(!passwordCheck.Succeeded)
                    serviceResult.Validation.Add(new JB2.Common.Validation("password", "Password") { IsValid = false });

                if(!serviceResult)
                {
                    throw new JB2.Common.Exceptions.ResultException("Validation Exception");
                }


                ApplicationUser newUser = new ApplicationUser() { Email = username, UserName = username, DisplayName = displayname, Birthdate = birthday };

                var result = await UserManager.CreateAsync(newUser, password);

                if (result.Succeeded)
                {
                    var authentication = HttpContext.GetOwinContext().Authentication;

                    authentication.SignIn(
                            new AuthenticationProperties { IsPersistent = true },
                            new ClaimsIdentity(new[] { new Claim(
                       ClaimsIdentity.DefaultNameClaimType, username) },
                               "Application"));

                    //await SignInManager.SignInAsync(newUser, isPersistent: false, rememberBrowser: false);
                }
            }
            catch(Exception ex)
            {
                

            }
            finally
            {
                ViewBag.ServiceResult = serviceResult;
            }

            return View("Login");

           


            //if (!string.IsNullOrEmpty(Request.Form["username"]))
            //{
            //    var user = new ApplicationUser { UserName = username, Email = model.Email };
            //    //user.FirstName = model.FirstName;
            //    //user.LastName = model.LastName;
            //    if (model.DisplayName == null)
            //    {
            //        user.DisplayName = user.FirstName + " " + user.LastName;
            //    }
            //    else
            //    {
            //        user.DisplayName = model.DisplayName;
            //    }
            //    user.CellPhone = model.CellPhone;

            //    var result = await UserManager.CreateAsync(user, model.Password);
            //    if (result.Succeeded)
            //    {
            //        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

            //        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
            //        // Send an email with this link
            //        string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
            //        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
            //        try
            //        {
            //            await UserManager.SendEmailAsync(user.Id, "New CITS Account",
            //                "Dear " + user.FirstName + " " + user.LastName + ",<br \\>" +
            //                "This is to confirm that your account for USDA Chemical Inventory Tracking System (CITS) is now active. The following username has been created for you: <br \\><br \\>" +
            //                "Username: " + user.UserName + " <br \\><br \\>" +
            //                "To sign in and create a new password, click or copy/paste the link into your browser: <a href=\"" + callbackUrl + "\">" + callbackUrl + "</a><br \\><br \\>" +
            //                "For more information or if you believe you have received this message in error, please contact your <a href=\"" + "mailto:admin@usda.org" + "\"> SOHES Administrator </a> at 301-504-6084.<br \\><br \\>" +
            //                "Thank you.");
            //        }
            //        catch (Exception ex)
            //        {
            //            ModelState.AddModelError("", ex.Message);
            //            return View(model);
            //        }
            //        //return View("ConfirmEmail");
            //        return RedirectToAction("Index", "Home");
            //    }
            //    AddErrors(result);
            //}

            //// If we got this far, something failed, redisplay form
            //return View(model);
            return View();
        }

        #endregion User
    }
}