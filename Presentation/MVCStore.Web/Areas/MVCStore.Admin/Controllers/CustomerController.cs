using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcContrib.UI.Grid;
using MVCStore.Admin.Models;
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

        public ActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCustomer(Customer model)
        {
            if (ModelState.IsValid)
            {
                string message = _authenticationService.Createuser(model.Username, model.Password, model.Password,
                    model.Question, model.Answer);

                ModelState.AddModelError("result", message);
                return RedirectToAction("Index", "Customer");
            }
            return PartialView("Index", model);
        }

        public ActionResult RoleList()
        {
            List<string> roles = _authenticationService.GetAllRoles();
            List<CustomerRole> roleList = roles.Select(role => new CustomerRole {RoleId = 0, RoleName = role}).ToList();
            return PartialView("RoleList", roleList);
        }

        public ActionResult ListUsers(string searchWord, GridSortOptions gridSortOptions, int? page)
        {
            var pagedViewModel = new PagedViewModel<Customer>
            {
                ViewData = ViewData,
                Query = _authenticationService.GetAllUsers().AsQueryable(),
                GridSortOptions = gridSortOptions,
                DefaultSortColumn = "Username",
                Page = page,
                PageSize = 2,
            }
                .Setup();

            return PartialView(pagedViewModel);
        }
    }
}