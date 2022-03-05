using System;


public interface IShape
{
    double getArea();
    bool isValid();
}

public class Rectangle : IShape
{
    private double width;
    private double length;

    public Rectangle(double _width, double _length)
    {
        if (_width <= 0 || _length <= 0)
            throw new ArgumentException("error arguments");
        width = _width;
        length = _length;
    }

    public double Width
    {
        get => width;
        set => width = value;
    }

    public double Length
    {
        get => length;
        set => length = value;
    }

    public double getArea()
    {
        return this.length * this.width;
    }

    public bool isValid()
    {
        if (this.width <= 0 || this.length <= 0)
            return false;
        return true;
    }
}

public class Square : IShape
{
    private double side;

    public Square(double _side)
    {
        if (_side <= 0)
            throw new ArgumentException("invalid side");
        side = _side;
    }
    public double Side
    {
        get => side;
        set => side = value;
    }

    public double getArea()
    {
        return side * side;
    }

    public bool isValid()
    {
        return side > 0 ? true : false;
    }
}

public class Circle : IShape
{
    private double r;
    public Circle(double _r){
        if (_r <= 0)
            throw new ArgumentException("invalid radius");
        r = _r;
    }
    public double R
    {
        get => r;
        set => r = value;
    }
    public double getArea()
    {
        return Math.PI*r*r;
    }

    public bool isValid()
    {
        return r > 0 ? true : false;
    }

}

public class ShapeFactory
{
    public IShape GetShape(String ShapeType, double arg1,double arg2=0)//?
    {
        if (ShapeType.Equals("Rectangle"))
            return new Rectangle(arg1,arg2);
        if (ShapeType.Equals("Square"))
            return new Square(arg1);
        if (ShapeType.Equals("Circle"))
            return new Circle(arg1);
        throw new ArgumentException("Invalid ShapeType");
    }
}

namespace homework3
{
    class Program
    {
        static void Main(string[] args)
        {
            int TypeIndex = 0;
            double arg1, arg2, total_area=0;
            Random RandomGenerator = new Random();
            IShape tmpShape;
            ShapeFactory Factory = new ShapeFactory();
            String[] type = { "Rectangle", "Square", "Circle" };
            arg1 = RandomGenerator.NextDouble() * RandomGenerator.Next();
            arg2 = RandomGenerator.NextDouble() * RandomGenerator.Next();
            for(int i = 0; i != 10; ++i)
            {
                TypeIndex = RandomGenerator.Next(3);
                try{
                    tmpShape = Factory.GetShape(type[TypeIndex], arg1, arg2);
                    total_area += tmpShape.getArea();
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            Console.WriteLine(total_area);
            return;
        }
    }
}