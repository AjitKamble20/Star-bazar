using StarBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StarBazar.Web.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public DateTime OrderedAt { get; set; }

        public string Status { get; set; }

        public decimal TotalAmount { get; set; }

        public virtual List<OrderItem> OrderItems { get; set; }
    }
}