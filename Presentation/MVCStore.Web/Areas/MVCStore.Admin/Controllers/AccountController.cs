using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCStore.Admin.Models;
using MVCStore.Data.Repository;
using System.Web.Security;
using MVCStore.Services;
using MVCStore.Services.Authentication;

namespace MVCStore.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        public AccountController(IAuthenticationService authenticationService)
        {
            this._authenticationService = authenticationService;
        }

        //
        // GET: /Admin/

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel loginModel, string returnUrl)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (_authenticationService.ValidateUser(loginModel.UserName, loginModel.Password))
                    {
                        FormsAuthentication.SetAuthCookie(loginModel.UserName, loginModel.RememberMe);
                        //FormsAuthentication.RedirectFromLoginPage(loginModel.UserName, true);

                        string redirectUrl = FormsAuthentication.GetRedirectUrl(loginModel.UserName, loginModel.RememberMe);

                        if (!string.IsNullOrWhiteSpace(redirectUrl))
                        {
                            Response.Redirect(redirectUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Dashboard");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid email or password. Please try again.");
                    }
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred during login. Please try again.");
            }

            return View(loginModel);
        }

        
        public ViewResult ForgotPassword()
        {
            return View();
        }

        public ActionResult LogOff()
        {
            LogoutUser();
            return RedirectToAction("Login", "Account");
        }

        private void LogoutUser()
        {
            FormsAuthentication.SignOut();

        }


    }
}
