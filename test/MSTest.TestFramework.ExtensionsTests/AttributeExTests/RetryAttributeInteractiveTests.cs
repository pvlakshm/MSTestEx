using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.TestFramework.Extensions.AttributeEx;
using MSTest.TestFramework.Extensions.TestMethodEx;
using Moq;
using System;

namespace MSTest.TestFramework.ExtensionsTests.AttributeExTests
{
    [TestClass]
    public class RetryAttributeInteractiveTests
    {
        [TestMethodEx]
        [Retry(2)]
        public void U_PassingTest_NoRetry()
        {
            Assert.IsTrue(true);
        }

        [TestMethodEx]
        [Retry(2)]
        public void U_FailingTest_RetriedTwice()
        {
            Assert.IsTrue(false);
        }

        [TestMethodEx]
        [Ignore]
        [Retry(2)]
        public void U_IgnoredTest()
        {
        }

        [TestMethodEx]
        [Retry(2)]
        public void U_InconclusiveTest_NoRetry()
        {
            Assert.Inconclusive();
        }

        [TestMethodEx]
        [Timeout(10)]
        public void U_LongRunningTest_NoRetry()
        {
            System.Threading.Thread.Sleep(1000 * 11);
        }

        [TestMethodEx]
        [ExpectedException(typeof(System.DivideByZeroException))]
        [Retry(2)]
        public void U_PassingTestWithExpectedException_NoRetry()
        {
            int num = 1;
            int denom = 0;
            int val = num / denom;
        }

        [TestMethodEx]
        [Retry(2)]
        public void U_FailingTestWithException_RetriedTwice()
        {
            int num = 1;
            int denom = 0;
            int val = num / denom;
        }
    }
}
