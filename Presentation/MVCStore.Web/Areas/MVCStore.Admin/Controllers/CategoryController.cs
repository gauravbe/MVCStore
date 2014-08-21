using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcContrib.UI.Grid;
using MVCStore.Admin.Models;
using MVCStore.Data.Entities;
using MVCStore.Services.Catalog;

namespace MVCStore.Admin.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        //
        // GET: /Category/
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }
      

        public ActionResult Index(string searchWord, GridSortOptions gridSortOptions, int? page)
        {
            //IEnumerable<Category> categories = _categoryService.FetchCategories();
            //return View(categories);

            var pagedViewModel = new PagedViewModel<Category>
            {
                ViewData = ViewData,
                Query = _categoryService.FetchCategories().AsQueryable(),
                GridSortOptions = gridSortOptions,
                DefaultSortColumn = "Name",
                Page = page,
                PageSize = 2,
            }                      
            .Setup();

            return View(pagedViewModel);
        }

         [HttpPost]
        public ActionResult Add(Category model)
        {
             if (ModelState.IsValid)
             {
                 _categoryService.SaveCategory(model);
             }
             return null;
        }
        
    }
}