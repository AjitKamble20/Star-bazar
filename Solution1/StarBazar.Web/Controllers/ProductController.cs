using StarBazar.Entities;
using StarBazar.Services;
using StarBazar.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StarBazar.Web.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductTable(string search, int? pageNo)
        {
            ProductSearchViewModel model = new ProductSearchViewModel();
            model.SearchTerm = search;

           model.PageNo= pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            var totalRecords = ProductService.Instance.GetProductsCount(search);
            model.Products = ProductService.Instance.GetProducts(model.PageNo);

            //model.Pager = new Pager(totalRecords, pageNo, pageSize);

            return PartialView(model);
        }
        #region Creation
        public ActionResult Create()
        {
            CategoryService categoryService = new CategoryService();
            NewProductViewModel model = new NewProductViewModel();

            model.AvailableCategories = categoryService.GetCategories();

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Create(NewProductViewModel vm)
        {
            CategoryService categoryService = new CategoryService();
            var newProduct = new Product();
            newProduct.Name = vm.Name;
            newProduct.Description = vm.Description;
            newProduct.Price = vm.Price;
            newProduct.Category = categoryService.GetCategoryById(vm.CategoryID);
            newProduct.ImageURL = vm.ImageURL;

            ProductService.Instance.SaveProduct(newProduct);
            return RedirectToAction("ProductTable");
        }
        #endregion

        #region Updation

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            CategoryService categoryService = new CategoryService();
            EditProductViewModel vm = new EditProductViewModel();

            var product = ProductService.Instance.GetProductById(Id);

            vm.ID = product.Id;
            vm.Name = product.Name;
            vm.Description = product.Description;
            vm.Price = product.Price;
            vm.CategoryID = product.Category != null ? product.Category.Id : 0;
            vm.ImageURL = product.ImageURL;

            vm.AvailableCategories = categoryService.GetCategories();

            return PartialView(vm);
        }

        [HttpPost]
        public ActionResult Edit(EditProductViewModel model)
        {
            var existingProduct = ProductService.Instance.GetProductById(model.ID);
            existingProduct.Name = model.Name;
            existingProduct.Description = model.Description;
            existingProduct.Price = model.Price;

            existingProduct.Category = null; //mark it null. Because the referncy key is changed below
            existingProduct.CategoryId = model.CategoryID;

            //dont update imageURL if its empty
            if (!string.IsNullOrEmpty(model.ImageURL))
            {
                existingProduct.ImageURL = model.ImageURL;
            }

            ProductService.Instance.UpdateProduct(existingProduct);

            return RedirectToAction("ProductTable");
        }

        #endregion
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            ProductService.Instance.DeleteProduct(Id);
            return RedirectToAction("ProductTable");
        }

        [HttpGet]
        public ActionResult Details(int Id)
        {
            ProductViewModel vm = new ProductViewModel();
            vm.Product = ProductService.Instance.GetProductById(Id);

            return View(vm);
        }

    }
}