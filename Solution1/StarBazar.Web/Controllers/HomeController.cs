using StarBazar.Services;
using StarBazar.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StarBazar.Web.Controllers
{
    public class HomeController : Controller
    {
        CategoryService categoryService = new CategoryService();
        public ActionResult Index()
        {
            HomeViewModel vm = new HomeViewModel();
            vm.FeaturedCategories = categoryService.GetFeaturedCategories();
            return View(vm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}