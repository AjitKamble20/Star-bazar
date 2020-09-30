using StarBazar.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarBazar.Database
{
   public class CbContext:DbContext,IDisposable
    {
        public CbContext() : base("StarBazar")
        {
        }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Config> Configrations { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<UserDetails> UserDetailses { get; set; }

        public DbSet<tbl_State> tbl_States { get; set; }

        public DbSet<tbl_District> tbl_Districts { get; set; }

        public DbSet<tbl_Pincode> tbl_Pincodes { get; set; }

        public DbSet<tbl_Location> tbl_Locations { get; set; }

        public DbSet<PincodeTable> PincodeTables { get; set; }
    }
}
