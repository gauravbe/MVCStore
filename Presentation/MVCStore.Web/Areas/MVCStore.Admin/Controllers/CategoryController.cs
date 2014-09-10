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
      

        //public ActionResult Index(string searchWord, GridSortOptions gridSortOptions, int? page)
        //{
        //    var pagedViewModel = new PagedViewModel<Category>
        //    {
        //        ViewData = ViewData,
        //        Query = _categoryService.FetchCategories().AsQueryable(),
        //        GridSortOptions = gridSortOptions,
        //        DefaultSortColumn = "Name",
        //        Page = page,
        //        PageSize = 2,
        //    }                      
        //    .Setup();

        //    return View(pagedViewModel);
        //}

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAllCategories(int pageNumber, int pageSize)
        {

            List<Category> categories = _categoryService.FetchCategories().OrderBy(o => o.ID).Skip<Category>(pageSize * (pageNumber - 1))
                .Take<Category>(pageSize).ToList<Category>();
            return Json(new
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = categories,
                RecordCount = _categoryService.FetchCategories().Count<Category>()
            }, JsonRequestBehavior.AllowGet);            
        }
        
        //** APPROACH 1 **//        
        //[HttpPost]
        //public ActionResult Add([JsonBinder] Category model)
        //{
        //    _categoryService.SaveCategory(model);
        //    return RedirectToAction("Index", "Category");            
        //}

        public JsonResult Add(Category model)
        {
            _categoryService.SaveCategory(model);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(Category model)
        {
            _categoryService.SaveCategory(model);
            return Json(_categoryService.FetchCategories(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddCategory()
        {
            return View();
        }        

        public JsonResult Delete(int id)
        {
            _categoryService.Delete(id);
            return Json(new { Status = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int id)
        {
            Category category = _categoryService.GetCategory(id);
            return View(category);
        }
    }
}