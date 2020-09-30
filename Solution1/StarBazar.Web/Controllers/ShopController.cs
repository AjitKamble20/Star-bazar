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
    public class ShopController : Controller
    {

        public ActionResult Index(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy, int? pageNo)
        {
            CategoryService categoryService = new CategoryService();
            ConfigService configService = new ConfigService();
            var pageSize = configService.ShopPageSize();

            ShopViewModel model = new ShopViewModel();

            model.SearchTerm = searchTerm;
            model.FeaturedCategories = categoryService.GetFeaturedCategories();
            model.MaximumPrice = ProductService.Instance.GetMaximumPrice();

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            model.SortBy = sortBy;
            model.CategoryID = categoryID;

            int totalCount = ProductService.Instance.SearchProductsCount(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy);
            model.Products = ProductService.Instance.SearchProducts(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy, pageNo.Value,10);

            model.Pager = new Pager(totalCount, pageNo, pageSize);

            return View(model);
        }

        public ActionResult FilterProducts(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy, int? pageNo)
        {
            ConfigService configService = new ConfigService();
            var pageSize = configService.ShopPageSize();

            FilterProductsViewModel model = new FilterProductsViewModel();

            model.SearchTerm = searchTerm;
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            model.SortBy = sortBy;
            model.CategoryID = categoryID;

            int totalCount = ProductService.Instance.SearchProductsCount(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy);
            model.Products = ProductService.Instance.SearchProducts(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy, pageNo.Value,10);

            model.Pager = new Pager(totalCount, pageNo, pageSize);

            return PartialView(model);
        }

        // GET: Shop
        public ActionResult CheckOut()

        {

            CheckOutViewModel vm = new CheckOutViewModel();
            var cartProductCookies = Request.Cookies["CartProducts"];
            if (cartProductCookies != null && !string.IsNullOrEmpty(cartProductCookies.Value))
            {
                // var productId = cartProductCookies.Value;
                //var ids = productId.Split('-');
                //                List<int> pIDs = ids.Select(x => int.Parse(x)).ToList();

                vm.CartProductIds = cartProductCookies.Value.Split('-').Select(x => int.Parse(x)).ToList();

                vm.CartProducts = ProductService.Instance.GetProductByCookies(vm.CartProductIds);
            }

            return View(vm);
        }

        public JsonResult PlaceOrder(string ProductIds)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            if (!string.IsNullOrEmpty(ProductIds))
            {
                var productQuantities = ProductIds.Split('-').Select(x => int.Parse(x)).ToList();

                var boughtProducts = ProductService.Instance.GetProducts(productQuantities.Distinct().ToList());

                Order newOrder = new Order();
                //newOrder.UserID = User.Identity.GetUserId();
                newOrder.OrderedAt = DateTime.Now;
                newOrder.Status = "Pending";
                newOrder.TotalAmount = boughtProducts.Sum(x => x.Price * productQuantities.Where(productID => productID == x.Id).Count());

                newOrder.OrderItems = new List<OrderItem>();
                newOrder.OrderItems.AddRange(boughtProducts.Select(x => new OrderItem() { ProductId = x.Id, Quantity = productQuantities.Where(productID => productID == x.Id).Count() }));

                var rowsEffected = ShopService.Instance.SaveOrder(newOrder);

                result.Data = new { Success = true, Rows = rowsEffected };
            }
            else
            {
                result.Data = new { Success = false };
            }

            return result;
        
    }
    }
}