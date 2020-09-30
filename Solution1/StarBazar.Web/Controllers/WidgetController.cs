using StarBazar.Services;
using StarBazar.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StarBazar.Web.Controllers
{
    public class WidgetController : Controller
    {
        // GET: Widget
        public ActionResult Products(bool isLatestProducts ,int? categoryId=0)
        {
            ProductWidgetViewModel vm = new ProductWidgetViewModel();

            if (isLatestProducts)
            {
                vm.Products = ProductService.Instance.GetLatestProducts(4);
            }
            else if (categoryId.HasValue && categoryId.Value > 0)
            {
                vm.Products = ProductService.Instance.GetProductsByCategory(categoryId.Value, 4);
            }
            else
            {
                vm.Products = ProductService.Instance.GetWidgetProducts(1,8);
            }

            return PartialView(vm);
        }
    }
}