using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static System.Math;

namespace NumberTheoryWPF
{
    public class MultiThreadingAnalysis
    {
        private bool fAnswerFound;
        private List<ulong> fAllPrimesFound;
        private ulong fNextNumberToCheck;
        private object fLock = new object();

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
                if (number % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public ulong CalculateLargestPrime(ulong number)
        {
            if (number < 4)
            {
                return number;
            }
                
            fNextNumberToCheck = number;
            List<Thread> threads = new List<Thread>();
            fAnswerFound = false;
            fAllPrimesFound = new List<ulong>();

            for (int i = 0; i < Environment.ProcessorCount; i++)
            {
                threads.Add(new Thread(CalculateLargestPrimeThread));
                threads[i].Name = String.Concat("Thread number ", i);
                threads[i].Start();               
            }
          
            foreach (Thread thread in threads)
            {
                thread.Join();
            }

            return fAnswerFound == false ? number : fAllPrimesFound.Max();
        }

        private void CalculateLargestPrimeThread()
        {
            ulong candidate;
            while (!fAnswerFound)
            {
                Monitor.Enter(fLock);               
                candidate = fNextNumberToCheck;
                fNextNumberToCheck--;
                Monitor.Exit(fLock);

                if (candidate < 2)
                {                  
                    break;
                }

                if (IsPrime(candidate))
                {
                    fAnswerFound = true;
                    fAllPrimesFound.Add(candidate);
                }
            }
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
