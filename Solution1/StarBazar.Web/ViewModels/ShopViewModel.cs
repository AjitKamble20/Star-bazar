﻿using StarBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StarBazar.Web.ViewModels
{
    public class CheckOutViewModel
    {

        public List<Product> CartProducts { get; set; }

        public List<int> CartProductIds { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }
    }
    public class ShopViewModel
    {
        public int MaximumPrice { get; set; }
        public List<Category> FeaturedCategories { get; set; }
        public List<Product> Products { get; set; }
        public int? SortBy { get; set; }
        public int? CategoryID { get; set; }

        public Pager Pager { get; set; }
        public string SearchTerm { get; set; }
    }

    public class FilterProductsViewModel
    {
        public List<Product> Products { get; set; }

        public Pager Pager { get; set; }
        public int? SortBy { get; set; }
        public int? CategoryID { get; set; }
        public string SearchTerm { get; set; }
    }
}