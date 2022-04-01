using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order
{
    class Program
    {
        static void Main(string[] args)
        {
            OrderDetails od1 = new OrderDetails("phone", 20, 400);
            OrderDetails od3 = new OrderDetails("trunk", 2, 3000.5);
            OrderDetails od2 = new OrderDetails("bike", 50, 150);
            Order o1 = new Order();
            o1.AddItem(od1);
            o1.AddItem(od2);
            Order o2 = new Order();
            o2.AddItem(od3);
            Order o3 = new();
            o3.AddItem(od1);
            OrderService os = new OrderService();
            os.AddOrder(o1);
            os.AddOrder(o2);
            os.ChangeOrder(o1, o3);
            //os.Sort();
            //o3.ItemList.Clear();

            os.Export("s.xml");
            //OrderService os2 = new OrderService();
            //os2.Import("s.xml");
            //foreach (Order o in os.SearchbyProduct("phone"))
            //{
            //    Console.WriteLine(o.ToString());
            //}

            //OrderService os = new();
            //Order o1 = new();
            //os.AddOrder(o1);
            //os.DeleteOrder(o1);
            //Console.WriteLine(os.SearchbyHashCode(o1.GetHashCode()).Count);
        }
    }
}
