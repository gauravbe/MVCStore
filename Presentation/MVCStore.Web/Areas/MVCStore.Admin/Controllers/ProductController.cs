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
    public class ProductController : Controller
    {
        //
        // GET: /Category/
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            this._productService = productService;
            this._categoryService = categoryService;

        }


        public ActionResult Index(string searchWord, GridSortOptions gridSortOptions, int? page)
        {
            var pagedViewModel = new PagedViewModel<Product>
            {
                ViewData = ViewData,
                Query = _productService.FetchProducts().AsQueryable(),
                GridSortOptions = gridSortOptions,
                DefaultSortColumn = "Name",
                Page = page,
                PageSize = 2,
            }
            .Setup();

            return View(pagedViewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(Product model)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["ImageContent"];
                _productService.SaveProduct(file, model);

                return RedirectToAction("Index", "Product");
            }
            IEnumerable<Category> categories = _categoryService.FetchCategories();
            ViewBag.CategoryId = new SelectList(categories, "ID", "Name");
            return View("AddProduct");
        }

        public ActionResult AddProduct()
        {
            IEnumerable<Category> categories = _categoryService.FetchCategories();
            ViewBag.CategoryId = new SelectList(categories, "ID", "Name");
            return View();
        }

        public ActionResult Edit(int id)
        {
            Product product = _productService.GetProduct(id);
            IEnumerable<Category> categories = _categoryService.FetchCategories();
            ViewBag.CategoryId = new SelectList(categories, "ID", "Name", product.CategoryId);
            return View(product);
        }

        public ActionResult Delete(int id)
        {
            _productService.Delete(id);
            return RedirectToAction("Index", "Product");
        }

        public ActionResult Details(int id)
        {            
            Product product = _productService.GetProduct(id);

            string name = _categoryService.GetCategory(product.CategoryId).Name;
            ViewBag.CategoryName = name;
            return View(product);
        }

        /// <summary>
        /// Retrive Image from database 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult RetrieveImage(int id)
        {
            byte[] cover = _productService.GetImageFromDataBase(id);
            if (cover != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
        }
    }
}