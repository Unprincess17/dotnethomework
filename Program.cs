using System;

namespace dotnet
{
    class Program
    {
        static void Main(string[] args)
        {
            int s1=0,s2=0,sd=0;
            string op="+";
            Console.WriteLine("s1: ");
            s1 = Int32.Parse(Console.ReadLine());
            Console.WriteLine("operator: ");
            op = Console.ReadLine();
            Console.WriteLine("s2: ");
            s2 = Int32.Parse(Console.ReadLine());
            switch (op[0])
            {
                case '+':
                    sd = s1 + s2;
                    break;
                case '-':
                    sd = s1 - s2;
                    break;
                case '*':
                    sd = s1 * s2;
                    break;
                case '/':
                    sd = s2 == 0 ? 0 : s1 / s2;
                    break;
            }
            Console.WriteLine($"output: {sd}");
        }
    }
}
