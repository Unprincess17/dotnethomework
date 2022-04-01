using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.IO;

namespace Order
{
    public class OrderService
    {
        private List<Order> OrderList = new();
        public OrderService()
        {
        }
        public List<Order>Show()
        {
            List<Order> rl = new();
            foreach(Order o in this.OrderList)
            {
                rl.Add(o);
            }
            return rl;
        }

        //query, manipulate, sort...
        public void AddOrder(Order o)
        {
            try
            {
                if (OrderList.Contains(o))
                {
                    throw new ArgumentException($"{o.ToString()} already exists");
                }
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
                Order tmp = Order.Copy(OrderList.Find((Order o) => o.Equals(OldOrder)));
                tmp.ItemList.Clear();
                foreach(Order.OrderItem od in NewOrder.ItemList)
                {
                    tmp.ItemList.Add(od);
                }
                //shallow?
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
        public List<Order> Sort()//by sum
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
        public List<Order> SearchbyHashCode(int HashCode)
        {
            var orders = from order in OrderList where order.GetHashCode() == HashCode orderby order.Sum() select order;
            return orders.ToList();
        }
        public void Export(String filename)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Order>));
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                xmlSerializer.Serialize(fs, OrderList);
            }
        }

        public void Import(String filename)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Order>));
            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                List<Order> orders = (List<Order>)xmlSerializer.Deserialize(fs);
                this.OrderList = orders;
            }
        }
    }
}
