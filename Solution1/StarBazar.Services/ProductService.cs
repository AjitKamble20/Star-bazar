using StarBazar.Database;
using StarBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace StarBazar.Services
{
  public class ProductService
    {
#region SingleTon
        public static ProductService Instance
        {
            get
            {
                if (instance == null) instance = new ProductService();

                return instance;
            }
        }
        private static ProductService instance { get; set; }

        private ProductService()
        {
        }
#endregion
        public Product GetProductById(int id)
        {
            using (var context = new CbContext())
            {
                return context.Products.Where(x => x.Id == id).Include(x => x.Category).FirstOrDefault();
            }
        }

        public List<Product> SearchProducts(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy, int pageNo,int pageSize)
        {
            using (var context = new CbContext())
            {
                var products = context.Products.ToList();

                if (categoryID.HasValue)
                {
                    products = products.Where(x => x.Category.Id == categoryID.Value).ToList();
                }

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    products = products.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
                }

                if (minimumPrice.HasValue)
                {
                    products = products.Where(x => x.Price >= minimumPrice.Value).ToList();
                }

                if (maximumPrice.HasValue)
                {
                    products = products.Where(x => x.Price <= maximumPrice.Value).ToList();
                }

                if (sortBy.HasValue)
                {
                    switch (sortBy.Value)
                    {
                        case 2:
                            products = products.OrderByDescending(x => x.Id).ToList();
                            break;
                        case 3:
                            products = products.OrderBy(x => x.Price).ToList();
                            break;
                        default:
                            products = products.OrderByDescending(x => x.Price).ToList();
                            break;
                    }
                }

                return products.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public int SearchProductsCount(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy)
        {
            using (var context = new CbContext())
            {
                var products = context.Products.ToList();

                if (categoryID.HasValue)
                {
                    products = products.Where(x => x.Category.Id == categoryID.Value).ToList();
                }

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    products = products.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
                }

                if (minimumPrice.HasValue)
                {
                    products = products.Where(x => x.Price >= minimumPrice.Value).ToList();
                }

                if (maximumPrice.HasValue)
                {
                    products = products.Where(x => x.Price <= maximumPrice.Value).ToList();
                }

                if (sortBy.HasValue)
                {
                    switch (sortBy.Value)
                    {
                        case 2:
                            products = products.OrderByDescending(x => x.Id).ToList();
                            break;
                        case 3:
                            products = products.OrderBy(x => x.Price).ToList();
                            break;
                        default:
                            products = products.OrderByDescending(x => x.Price).ToList();
                            break;
                    }
                }

                return products.Count;
            }
        }

        public List<Product> GetProducts(int pageNo)
        {
            int pageSize = 5;// int.Parse(ConfigurationsService.Instance.GetConfig("ListingPageSize").Value);

            using (var context = new CbContext())
            {
                //return context.Products.OrderBy(x => x.Id).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.Category).ToList();
                return context.Products.Include(x => x.Category).ToList();
            }
        }

        public List<Product> GetLatestProducts(int noOfProducts)
        {
            using (var context = new CbContext())
            {
                return context.Products.OrderByDescending(x => x.Id).Take(noOfProducts).Include(x => x.Category).ToList();
            }
        }

        public List<Product> GetWidgetProducts(int pageNo,int pageSize)
        {
            using (var context = new CbContext())
            {
                return context.Products.OrderByDescending(x => x.Id).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.Category).ToList();
            }
        }

        public List<Product> GetProductByCookies(List<int> IDs)
        {
            using (var context = new CbContext())
            {
                return context.Products.Where(x => IDs.Contains(x.Id)).ToList();
            }
        }

        public void SaveProduct(Product product)
        {
            using (var context = new CbContext())
            {
                context.Entry(product.Category).State = System.Data.Entity.EntityState.Unchanged;
                context.Products.Add(product);
                context.SaveChanges();
            }
        }
        public void UpdateProduct(Product product)
        {
            using (var context = new CbContext())
            {
                context.Entry(product).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteProduct(int id)
        {
            using (var context = new CbContext())
            {
                var productDelete = context.Products.Find(id);
                context.Products.Remove(productDelete);
                context.SaveChanges();
            }
        }

        public int GetProductsCount(string search)
        {
            using (var context = new CbContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Products.Where(product => product.Name != null &&
                         product.Name.ToLower().Contains(search.ToLower()))
                         .Count();
                }
                else
                {
                    return context.Products.Count();
                }
            }
        }

        public List<Product> GetProductsByCategory(int categoryID, int pageSize)
        {
            using (var context = new CbContext())
            {
                return context.Products.Where(x => x.Category.Id == categoryID).OrderByDescending(x => x.Id).Take(pageSize).Include(x => x.Category).ToList();
            }
        }

        public int GetMaximumPrice()
        {
            using (var context = new CbContext())
            {
                return (int)(context.Products.Max(x => x.Price));
            }
        }

        public List<Product> GetProducts(List<int> IDs)
        {
            using (var context = new CbContext())
            {
                return context.Products.Where(product => IDs.Contains(product.Id)).ToList();
            }
        }
    }


}
