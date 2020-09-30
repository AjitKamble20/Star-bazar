using StarBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StarBazar.Web.ViewModels
{
    public class ProductWidgetViewModel
    {
        public List<Product> Products { get; set; }

        public bool IsLatestProduct { get; set; }
    }
}