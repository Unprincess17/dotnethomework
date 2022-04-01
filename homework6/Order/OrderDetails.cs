using System;

namespace Order
{   
    [Serializable]
    public class OrderDetails
    {
        public String Name { get; set; }
        public int Number { get; set; }
        public double Price { get; set; }
        public OrderDetails()
        {
            Name = "";
            Number = 0;
            Price = 0;
        }
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
}
