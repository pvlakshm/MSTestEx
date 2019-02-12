using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace MSTest.TestFramework.Extensions.TestRunEx
{
    /// <summary>A retriable test method. The test will be rerun if it fails, up to a maximum number of times.</summary>
    /// <param name="retryCount">the number of times to rerun a test if it fails.</param>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class RetriableTestMethodAttribute : TestMethodAttribute
    {
        private int retryCount;

        public RetriableTestMethodAttribute(int retryCount)
        {
            if (retryCount <= 0)
                retryCount = 1;

            this.retryCount = retryCount;
        }

        public override TestResult[] Execute(ITestMethod testMethod)
        {
            var currentCount = 0;
            TestResult[] result = null;

            while (currentCount <= retryCount)
            {
                try
                {
                    result = base.Execute(testMethod);
                }
                catch (Exception) {}

                currentCount++;

                if (result.Any((tr) => tr.Outcome == UnitTestOutcome.Failed))
                {
                    continue;
                }
                else
                {
                    break;
                }
            }

            return result;
        }
    }
}
