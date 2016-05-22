using System;
using System.Diagnostics;

namespace NumberTheoryWPF
{
    public class MethodTimerWrapper<T>
    {
        private readonly Stopwatch fStopWatch;
        private readonly Func<ulong, T> fMethod;
        private readonly ulong fNumber;
        private TimeSpan fTimeforCalculation;
        private T fResult;
        
        public MethodTimerWrapper(Func<ulong, T> method, ulong number)
        {
            fStopWatch = new Stopwatch();
            fMethod = method;
            fNumber = number;
        }

        public MethodTimerWrapper()
        {
            fStopWatch = new Stopwatch();
        }

        public TimeSpan MethodExecutionTime
        {
            get { return fTimeforCalculation; }
        }

        public T Result
        {
            get { return fResult; }
        }

        public T ExecuteMethod()
        {
            fStopWatch.Start();
            fResult = fMethod(fNumber);
            fStopWatch.Stop();
            fTimeforCalculation = fStopWatch.Elapsed;
            return fResult;
        }

        public T ExecuteMethod(Func<ulong, T> method, ulong number)
        {           
            fStopWatch.Start();
            fResult = method(number);
            fStopWatch.Stop();
            fTimeforCalculation = fStopWatch.Elapsed;
            return fResult;
        }
    }
}
