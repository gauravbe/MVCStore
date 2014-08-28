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
        public ProductController(IProductService productService)
        {
            this._productService = productService;
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
            return View("AddProduct");
        }

        public ActionResult AddProduct()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            Product product = _productService.GetProduct(id);
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