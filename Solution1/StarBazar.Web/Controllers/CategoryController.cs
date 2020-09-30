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
    public class CategoryController : Controller
    {
        CategoryService categoryService = new CategoryService();
        // GET: Category

        public ActionResult CategoryTable(string search)
        {
            var categories = categoryService.GetCategories();
            if (string.IsNullOrEmpty(search) == false)
            {
                categories = categories.Where(p => p.Name != null && p.Name.ToLower().Contains(search.ToLower())).ToList();
            }
            return PartialView(categories);
        }

        public ActionResult Index()
        {
            var categoryList = categoryService.GetCategories();
            return View(categoryList);
        }
        public ActionResult Create()
        {
            var categoryList = categoryService.GetCategories();
            return PartialView(categoryList);
        }

        [HttpPost]
        public ActionResult Create(NewCategoryViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var newCategory = new Category();
                newCategory.Name = vm.Name;
                newCategory.Description = vm.Description;
                newCategory.ImageURL = vm.ImageURL;
                newCategory.isFeatured = vm.isFeatured;

                categoryService.SaveCategory(newCategory);

                return RedirectToAction("CategoryTable");
            }
            else
            {
                return new HttpStatusCodeResult(500);
            }
        }

        public ActionResult Edit(int id)
        {
            EditCategoryViewModel model = new EditCategoryViewModel();

            var category = categoryService.GetCategoryById(id);

            model.Id = category.Id;
            model.Name = category.Name;
            model.Description = category.Description;
            model.ImageURL = category.ImageURL;
            model.isFeatured = category.isFeatured;
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Edit(EditCategoryViewModel vm)
        {
            var existingCategory = categoryService.GetCategoryById(vm.Id);
            existingCategory.Name = vm.Name;
            existingCategory.Description = vm.Description;
            existingCategory.ImageURL = vm.ImageURL;
            existingCategory.isFeatured = vm.isFeatured;

            categoryService.UpdateCategory(existingCategory);

            return RedirectToAction("CategoryTable");
        }


        [HttpPost]
        public ActionResult Delete(int Id)
        {
            categoryService.DeleteCategory(Id);
            return RedirectToAction("CategoryTable");
        }
        
    }
}