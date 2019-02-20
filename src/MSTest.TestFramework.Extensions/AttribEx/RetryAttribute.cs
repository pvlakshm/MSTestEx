using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTest.TestFramework.Extensions.AttributeEx
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class RetryAttribute : TestPropertyAttribute
    {
        private const int MIN_RETRY_COUNT = 1;
        private const int MAX_RETRY_COUNT = 10;

        public RetryAttribute(int retryCount)
            : base("RetryAttribute", sanitize(retryCount, MIN_RETRY_COUNT, MAX_RETRY_COUNT))
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
