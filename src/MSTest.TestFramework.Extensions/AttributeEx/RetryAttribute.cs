using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTest.TestFramework.Extensions.AttributeEx
{
    /// <summary>- The RetryAttribute is used on a test method to specify that it should be rerun if it fails, up to a maximum number of times.</summary>
    /// <param name="bugId">int representing the retry count.</param>"
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
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
