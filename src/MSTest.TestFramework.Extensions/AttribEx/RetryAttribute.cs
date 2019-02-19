using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTest.TestFramework.Extensions.AttributeEx
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class RetryAttribute : TestPropertyAttribute
    {
        private const int MIN = 1;
        private const int MAX = 10;

        public RetryAttribute(int retryCount)
            : base("RetryAttribute", sanitize(retryCount, MIN, MAX))
        {
        }

        private static string sanitize(int retryCount, int min, int max)
        {
            if (retryCount < min && max < retryCount)
            {
                retryCount = min;
            }

            return retryCount.ToString();
        }
    }
}
