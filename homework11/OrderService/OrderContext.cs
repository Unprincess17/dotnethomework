using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp
{
    class OrderContext: DbContext
    {
        public OrderContext() : base("OrderDataBase")
        {
            Database.SetInitializer<OrderContext>(
                new DropCreateDatabaseAlways<OrderContext>());
            //Database.SetInitializer(
            ////new DropCreateDatabaseIfModelChanges<BloggingContext>());
            //new DropCreateDatabaseAlways<BloggingContext>());
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
