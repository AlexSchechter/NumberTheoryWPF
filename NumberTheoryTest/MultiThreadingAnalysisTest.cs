using NumberTheoryWPF;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberTheoryWPFTest
{
    [TestFixture]
    public class MultiThreadingAnalysisTest
    {
        private MultiThreadingAnalysis fAnalysis;

        [TestCase((ulong)0, false)]
        [TestCase((ulong)1, true)]
        [TestCase((ulong)2, true)]
        [TestCase((ulong)4, false)]
        [TestCase((ulong)17, true)]
        [TestCase((ulong)25, false)]
        [TestCase(18446744073709551615, false)]
        public void AnalysisClassVerifiesIfPrime(ulong inputValue, bool expectedResult)
        {
            GivenAnalysisClass();
            ThenItKnowsIfPrime(inputValue, expectedResult);
        }

        [Test]
        public void AnalysisClassReturnsPrimeFactors()
        {
            GivenAnalysisClass();
            ThenCalculatePrimeFactorsReturnsCorrectList();
        }

        [TestCase((ulong)0, (ulong)0)]
        [TestCase((ulong)1, (ulong)1)]
        [TestCase((ulong)2, (ulong)2)]
        [TestCase((ulong)4, (ulong)3)]
        [TestCase((ulong)6, (ulong)5)]
        [TestCase((ulong)81, (ulong)79)]
        public void AnalysisClassReturnsLargestPrimeSmallerThanInteger(ulong inputValue, ulong expectedResult)
        {
            GivenAnalysisClass();
            ThenItReturnsLargestPrimeSmallerThanInput(inputValue, expectedResult);
        }

        private void GivenAnalysisClass()
        {
            fAnalysis = new MultiThreadingAnalysis();
        }

        private void ThenItKnowsIfPrime(ulong inputValue, bool expectedResult)
        {
            Assert.AreEqual(fAnalysis.IsPrime(inputValue), expectedResult);
        }

        private void ThenCalculatePrimeFactorsReturnsCorrectList()
        {
            List<ulong> correctPrimeFactors = new List<ulong> { 2, 2, 3, 7 };
            CollectionAssert.AreEqual(fAnalysis.CalculatePrimeFactors(84), correctPrimeFactors);
        }

        private void ThenItReturnsLargestPrimeSmallerThanInput(ulong inputValue, ulong expectedResult)
        {
            Assert.AreEqual(fAnalysis.CalculateLargestPrime(inputValue), expectedResult);
        }
    }
}