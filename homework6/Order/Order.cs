using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Order
{
    [Serializable]
    public class Order
    {
        public struct OrderItem
        {
            public int OrderID { get; set; }
            public OrderDetails Info { get; set; }
        }
        public List<OrderItem> ItemList = new List<OrderItem>();
        public Order()
        {
        }
        public static Order Copy(Order o)
        {
            Order tmp = new();
            foreach (Order.OrderItem od in o.ItemList)
            {
                tmp.ItemList.Add(od);
            }
            return tmp;
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
            foreach(OrderItem item in ItemList)
            {
                rsb.Append(item.Info.Name); rsb.AppendLine(item.Info.Number.ToString());
            }
            return rsb.ToString();
        }
    }
}
