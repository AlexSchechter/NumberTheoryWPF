using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using static System.Math;

namespace NumberTheory
{
    public class Analysis
    {        
        public bool IsPrime(ulong number)
        {
            if (number == 0)
            {
                return false;
            }

            if (number <= 3)
            {
                return true;
            }
                
            for (ulong i = 2; i <= Floor(Sqrt(number)); i++)
            {
                if(number % i == 0)
                {
                    return false;
                }               
            }
            return true; 
        }

        public ulong CalculateLargestPrime(ulong number)
        {
            for (ulong i = number; i > 2; i--)
            {
                if (IsPrime(i))
                {
                    return i;
                }
            }

            return number;
        }

        public List<ulong> CalculatePrimeFactors(ulong number)
        {
            List<ulong> primeFactors = new List<ulong>();
            ulong rest = number;
            ulong nextPrimeFactor = 0;

            do
            {
                nextPrimeFactor = CalculateSmallestPrimeFactor(rest);
                primeFactors.Add(nextPrimeFactor);
                rest /= nextPrimeFactor;
            } while (rest != 1);

            return primeFactors;
        }

        private ulong CalculateSmallestPrimeFactor(ulong number)
        {
            for (ulong i = 2; i <= Floor(Sqrt(number)); i++)
            {
                if (number % i == 0)
                {
                    return i;
                }
            }

            return number;
        }
    }
}
