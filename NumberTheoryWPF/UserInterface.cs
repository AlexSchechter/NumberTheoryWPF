using NumberTheoryWPF;
using System;
using System.Collections.Generic;

namespace NumberTheoryWPF
{
    public class UserInterface
    {
        readonly Analysis fAnalysis;
        readonly MultiThreadingAnalysis fParallelAnalysis;

        public UserInterface()
        {
            fAnalysis = new Analysis();
            fParallelAnalysis = new MultiThreadingAnalysis();    
        }

        public void UserMenu()
        {
            int menuChoice;
            do
            {
                Console.Clear();
                Console.WriteLine("1. Check if an integer is a prime number");
                Console.WriteLine("2. Return the list of prime factors for an integer");
                Console.WriteLine("3. Return the largest prime number smaller or equal to an integer");
                Console.WriteLine("4. Exit application");
                Console.Write("{0}Pick your choice: ", Environment.NewLine);               
            } while (!(Int32.TryParse(Console.ReadLine(), out menuChoice) && menuChoice >= 1 && menuChoice <= 4));

            if (menuChoice == 4)
            {
                return;
            }

            ulong integerInput;
            do
            {
                Console.Write("{0}Please enter a positive integer between 2 and 18,446,744,073,709,551,615: ", Environment.NewLine);
            } while (!(UInt64.TryParse(Console.ReadLine(), out integerInput) && integerInput >= 2));

            Console.WriteLine();
            ProcessUserChoice(integerInput, menuChoice);                      
        }

        private void ProcessUserChoice(ulong integerInput, int menuChoice)
        {          
            switch (menuChoice)
            {               
                case 1:
                    MethodTimerWrapper<bool> isPrimeWrapper = new MethodTimerWrapper<bool>(fAnalysis.IsPrime, integerInput);
                    if (isPrimeWrapper.ExecuteMethod())
                    {
                        Console.WriteLine("{0} is a prime number", integerInput);
                    }
                    else
                    {
                        Console.WriteLine("{0} is not a prime number", integerInput);
                    }
                    Console.WriteLine("{0}The calculation lasted {1} seconds", Environment.NewLine, isPrimeWrapper.MethodExecutionTime);
                    break;

                case 2:
                    MethodTimerWrapper<List<ulong>> calculatePrimeFactorsWrapper = new MethodTimerWrapper<List<ulong>>(fAnalysis.CalculatePrimeFactors, integerInput);
                    List<ulong> primeFactors = calculatePrimeFactorsWrapper.ExecuteMethod();
                    Console.WriteLine("The prime factors for {0} are :", integerInput);
                    foreach (ulong primeNumber in primeFactors)
                    {
                        Console.Write(primeNumber + " ");
                    }
                    Console.WriteLine();
                    Console.WriteLine("{0}The calculation lasted {1} seconds", Environment.NewLine, calculatePrimeFactorsWrapper.MethodExecutionTime);
                    break;

                case 3:
                    MethodTimerWrapper<ulong> calculateLargestPrimeWrapper = new MethodTimerWrapper<ulong>(fAnalysis.CalculateLargestPrime, integerInput);
                    MethodTimerWrapper<ulong> calculateLargestPrimeWrapperParallel = new MethodTimerWrapper<ulong>(fParallelAnalysis.CalculateLargestPrime, integerInput);
                    Console.WriteLine("The largest prime that is smaller than {0} is {1}.", integerInput, calculateLargestPrimeWrapper.ExecuteMethod());
                    Console.WriteLine("The largest prime that is smaller than {0} is {1}.", integerInput, calculateLargestPrimeWrapperParallel.ExecuteMethod());
                    Console.WriteLine("{0}The calculation lasted {1} seconds in the sequential calculation", Environment.NewLine, calculateLargestPrimeWrapper.MethodExecutionTime);
                    Console.WriteLine("{0}The calculation lasted {1} seconds in the parallel calculation", Environment.NewLine, calculateLargestPrimeWrapperParallel.MethodExecutionTime);
                    break;
            }
          
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
            UserMenu();
        }
    }
}
