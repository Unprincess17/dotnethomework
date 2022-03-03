using System;

namespace PrimeFactors
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(getNextPrime(19));

            int input = Int32.Parse(Console.ReadLine());
            R_getFactor(input, 2);

        }

        private static void R_getFactor(int input,int prime)
        {
            if (prime > input)//退出
                return;
            if(input%prime == 0)//可整除
            {
                Console.WriteLine(prime.ToString());
                R_getFactor(input / prime, prime);
            }
            if((input/prime)*prime < input)//不可整除,寻找下一个prime
            {
                R_getFactor(input, getNextPrime(prime));
            }
        }

        private static int getNextPrime(int prime)
        {
            int re_prime = prime,tmp;//start from prime
            //re_prime plus 2, break if re_prime is a prime
            if (prime == 2)
                return 3;
            re_prime += 2;
            for (tmp = 2; tmp != re_prime; ++tmp)//if is prime, then return re_prime
            {
                if (re_prime / tmp == Convert.ToDouble(re_prime) / tmp)//能整除 
                {
                    tmp=1;re_prime += 2;
                }
            }
            return re_prime;            
        }
    }
}
