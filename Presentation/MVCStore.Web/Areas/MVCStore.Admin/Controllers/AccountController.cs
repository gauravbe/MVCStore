using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCStore.Admin.Models;
using MVCStore.Data.Repository;
using System.Web.Security;

namespace MVCStore.Admin.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
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
            if (ModelState.IsValid)
            {
                UserRepository userRepository = new UserRepository();
                if (userRepository.ValidateUser(loginModel.UserName, loginModel.Password))
                {
                    FormsAuthentication.RedirectFromLoginPage(loginModel.UserName, true);
                    //return RedirectToLocal(returnUrl);
                }
            }
            return null;
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
