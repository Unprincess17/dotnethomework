using Microsoft.VisualStudio.TestTools.UnitTesting;
using Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Tests
{
    [TestClass()]
    public class OrderServiceTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void AddOrderTest()
        {
            //how to test it if I cannot add?
            //arrange
            OrderService os = new();
            Order o = new();
            OrderDetails od = new();
            o.AddItem(od);
            //act
            os.AddOrder(o);
            os.AddOrder(o);

        }

        [TestMethod()]
        public void ChangeOrderTest()
        {
            OrderService os = new();
            Order o = new();
            o.AddItem(new OrderDetails("car", 1, 1));
            o.AddItem(new OrderDetails("computer", 10, 2));
            Order o1 = new();
            o1.AddItem(new OrderDetails("bus", 2, 1));
            o1.AddItem(new OrderDetails("mac", 11, 2));
            //act
            os.AddOrder(o);
            os.ChangeOrder(o, o1);
            //assert
            Assert.IsTrue(os.SearchbyHashCode(o1.GetHashCode()) != null);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ChangeOrderTestException()
        {
            OrderService os = new();
            Order o = new();
            o.AddItem(new OrderDetails("car", 1, 1));
            o.AddItem(new OrderDetails("computer", 10, 2));
            Order o1 = new();
            o.AddItem(new OrderDetails("bus", 2, 1));
            o.AddItem(new OrderDetails("mac", 11, 2));
            Order o3 = new Order();
            //act
            os.AddOrder(o);
            os.ChangeOrder(o3, o1);
        }

        [TestMethod()]
        public void DeleteOrderTest()
        {
            OrderService os = new();
            Order o1 = new();
            os.AddOrder(o1);
            os.DeleteOrder(o1);
            Assert.IsTrue(os.SearchbyHashCode(o1.GetHashCode()).Count == 0);
        }

        [TestMethod()]
        public void SortTest()
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
            OrderService os1 = new();
            os1.AddOrder(o2);
            os1.AddOrder(o3);

            os.Sort();

            List<Order> l1 = os.Show();
            List<Order> l2 = os1.Show();
            Assert.IsTrue(l1.Equals(l2));
        }

        [TestMethod()]
        public void SearchbyProductTest()
        {
            OrderDetails od1 = new("bike", 50, 50);
            Order o1 = new();
            o1.AddItem(od1);
            OrderService os = new();
            os.AddOrder(o1);

            Assert.AreEqual((os.SearchbyProduct(od1.Name))[0], od1);
        }

        [TestMethod()]
        public void SearchbyHashCodeTest()
        {
            OrderDetails od1 = new("bike", 50, 50);
            Order o1 = new();
            o1.AddItem(od1);
            OrderService os = new();
            os.AddOrder(o1);

            Assert.AreEqual((os.SearchbyHashCode(od1.GetHashCode()))[0], od1);
        }

        [TestMethod()]
        public void ExportTest()
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

            os.Export("s1.xml");
            Assert.AreEqual(@"E:\c#\dotnet\Order\bin\Debug\net5.0", "s1.xml");
        }

        [TestMethod()]
        public void ImportTest()
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
            OrderService os1 = new();
            os1.Import(@"E:\c#\dotnet\Order\bin\Debug\net5.0\s.xml");
            Assert.AreEqual(os.Show(),os1.Show());
        }
    }
}