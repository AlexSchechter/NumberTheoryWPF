using NUnit.Framework;
using NumberTheoryWPF;
using System;
using System.Threading;

namespace NumberTheoryWPFTest
{
    [TestFixture]
    public class MethodTimerWrapperTest
    {
        private MethodTimerWrapper<double> ftestMethodTimerWrapper;
        private const double methodExecutionTimeBoundary = 0.001;

        [Test]
        public void ExecuteMethodReturnsCorrectResultForGivenMethodAndInput()
        {
            GivenMethodTimerWrapper();
            ReturnsCorrectMethodResult();
        }

        [Test]
        public void ExecuteMethodSetsMethodExcecutionTimePropertyCorrectly()
        {
            GivenMethodTimerWrapper();
            WhenExecuteFunctionIsCalled();
            ReturnsCorrectMethodExecutionTime();
        }

        private double TestMethodResult(ulong number)
        {
            Thread.Sleep(1000);
            return Math.Sqrt(number);
        }
        
        private void GivenMethodTimerWrapper()
        {
            ftestMethodTimerWrapper = new MethodTimerWrapper<double>(TestMethodResult, 16);
        }

        private void WhenExecuteFunctionIsCalled()
        {
            ftestMethodTimerWrapper.ExecuteMethod();          
        }

        private void ReturnsCorrectMethodResult()
        {
            Assert.AreEqual(4, ftestMethodTimerWrapper.ExecuteMethod());
        }     
        
        private void ReturnsCorrectMethodExecutionTime()
        {
            Assert.That(1, Is.EqualTo(ftestMethodTimerWrapper.MethodExecutionTime.TotalSeconds).Within(methodExecutionTimeBoundary));
        }
    }
}
