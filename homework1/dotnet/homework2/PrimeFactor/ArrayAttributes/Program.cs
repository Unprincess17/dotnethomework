using System;

namespace ArrayAttributes
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 3, 4, 32, 241, -423, 314 };
            int max, min, total, avg;
            GetAttributes(arr,out max,out min,out total,out avg);
            Console.WriteLine($"{max} {min} {total} {avg}");
        }

        static void GetAttributes(int[] array, out int max, out int min, out int total, out int avg)
        {
            max = Int32.MinValue; min = Int32.MaxValue; total = 0;
            foreach(int i in array)
            {
                total += i;
                max = i > max ? i : max;
                min = i < min ? i : min;
            }
            avg = total / array.Length;
        }
    }
}
