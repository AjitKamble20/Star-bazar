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
  public class OrderService
    {
#region SingleTon
        public static OrderService Instance
        {
            get
            {
                if (instance == null) instance = new OrderService();

                return instance;
            }
        }
        private static OrderService instance { get; set; }

        private OrderService()
        {
        }
#endregion
        
        public List<Order> GetProducts()
        {
            using (var context = new CbContext())
            {
                return context.Orders.ToList();
            }
        }


        public void SaveOrder(Order order)
        {
            using (var context = new CbContext())
            {
                context.Orders.Add(order);
                context.SaveChanges();
            }
        }

    }


}
