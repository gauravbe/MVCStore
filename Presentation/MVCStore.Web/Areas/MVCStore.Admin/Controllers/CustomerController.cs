using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCStore.Data.Entities;
using MVCStore.Services.Authentication;

namespace MVCStore.Admin.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        // GET: /Category/
        private readonly IAuthenticationService _authenticationService;
        public CustomerController(IAuthenticationService authenticationService)
        {
            this._authenticationService = authenticationService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Customer model)
        {
            if (ModelState.IsValid)
            {
                string message = _authenticationService.Createuser(model.Username, model.Password, model.Password,
                    model.Question, model.Answer);

                ModelState.AddModelError("result", message);
                return RedirectToAction("Index", "Customer");
            }
            return View("Index", model);
        }

        public ActionResult RoleList()
        {
            List<string> roles = _authenticationService.GetAllRoles();
            List<CustomerRole> roleList = roles.Select(role => new CustomerRole {RoleId = 0, RoleName = role}).ToList();
            return View("RoleList", roleList);
        }       
    }
}
