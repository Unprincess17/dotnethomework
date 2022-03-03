using System;

namespace getPrimes
{
    class Program
    {
        static void Main(string[] args)
        {
            getPrimes(100);
        }

        //idx1:iter from 1->100;
        //idx2:iter from 2->100
        //idx3:iter from 2->100
        private static void getPrimes(int EndNumber)
        {
            int StartNumber = 2, idx1,idx2,times;
            bool[] isPrimeArray = new bool[EndNumber+1];
            for (idx1 = 2; idx1 != EndNumber+1; ++idx1)
                isPrimeArray[idx1] = true;
            for(idx2 = 2; Math.Pow(idx2,2)<EndNumber+1; ++idx2)
            {
                for (times = 2; idx2 * times < EndNumber+1; ++times)
                {
                    isPrimeArray[idx2 * times] = false;
                }
            }
            for(idx1 = 2; idx1 != EndNumber+1; ++idx1)
            {
                if (isPrimeArray[idx1])
                {
                    Console.Write(idx1);
                    Console.Write(' ');
                }
            }
            
        }
    }
}
