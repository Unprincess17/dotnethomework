using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Order
{

    class Order
    {
        public struct OrderItem
        {
            public int OrderID { get; set; }
            public OrderDetails Info { get; set; }
        }
        public List<OrderItem> ItemList;
        public Order()
        {
            ItemList = new List<OrderItem>();
        }
        public double Sum()
        {
            double sum = 0;
            foreach(OrderItem i in ItemList)
            {
                sum += i.Info.Number * i.Info.Price;
            }
            return sum;
        }
        public void AddItem(OrderDetails d)
        {
            try {
                OrderItem i = new OrderItem();
                i.Info = d;
                i.OrderID = ItemList.Count();
                ItemList.Add(i);
            }
            catch
            {
                throw new ArgumentException("Invalid order item");
            }
        }
        public override bool Equals(object obj)
        {
            return obj is Order order &&
                   EqualityComparer<List<OrderItem>>.Default.Equals(ItemList, order.ItemList);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ItemList);
        }
        public override String ToString()
        {
            StringBuilder rsb = new StringBuilder();
            foreach(OrderDetails item in ItemList)
            {
                rsb.Append(item.Name); rsb.AppendLine(item.Number.ToString());
            }
            return rsb.ToString();
        }
    }

    class OrderDetails
    {
        public String Name { get; set; }
        public int Number { get; set; }
        public double Price { get; set; }
        public OrderDetails(String product, int number, double price)
        {
            Name = product;
            Number = number;
            Price = price;
        }

        public override bool Equals(object obj)
        {
            return obj is OrderDetails details &&
                   Name == details.Name &&
                   Number == details.Number;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Number);
        }

        public override String ToString()
        {
            return Name + Number;
        }
    }

    class OrderService
    {
        public List<Order> OrderList;
        public OrderService()
        {
            OrderList = new List<Order>();
        }
        //query, manipulate, sort...
        public void AddOrder(Order o)
        {
            try
            {
                OrderList.Add(o);
            }
            catch
            {
                throw new ArgumentException("Can not add order {0}", o.GetHashCode().ToString());
            }
        }
        public void ChangeOrder(Order OldOrder, Order NewOrder)
        {
            try
            {
                OrderList.Find((Order o) => o.Equals(OldOrder)).ItemList = NewOrder.ItemList;//shallow?
            }
            catch
            {
                throw new ArgumentException("Cannot find the order {0} to change", OldOrder.GetHashCode().ToString());
            }
        }
        public void DeleteOrder(Order order)
        {
            OrderList.Remove(order);
            //no need to throw exception if "order" dosen't exists, since it is already been deleted    
        }
        public List<Order> Sort()
        {
            List<Order> rsort = new List<Order>(OrderList);//shallow?
            rsort.Sort((a1, a2) => (int)(a1.Sum() - a2.Sum()));
            return rsort;
        }

        public List<Order> SearchbyProduct(string Name)
        {
            var orders = from order in OrderList where order.ItemList.Exists(item => item.Info.Name.Equals(Name)) orderby order.Sum() select order;
            List<Order> rl = orders.ToList();
            return rl;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            OrderDetails od1 = new OrderDetails("phone", 20, 400);
            OrderDetails od2 = new OrderDetails("trunk", 2, 3000.5);
            OrderDetails od3 = new OrderDetails("bike", 50, 150);
            Order o1 = new Order();
            o1.AddItem(od1);
            o1.AddItem(od2);
            Order o2 = new Order();
            o2.AddItem(od3);
            OrderService os = new OrderService();
            os.AddOrder(o1);
            os.AddOrder(o2);
            foreach(Order o in os.SearchbyProduct("phone"))
            {
                Console.WriteLine(o.ToString());
            }

        }
    }
}
