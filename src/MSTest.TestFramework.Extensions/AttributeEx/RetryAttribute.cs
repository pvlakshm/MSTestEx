using System;

namespace MSTest.TestFramework.Extensions.AttributeEx
{
    /// <summary>- The RetryAttribute is used on a test method to specify that it should be rerun if it fails, up to a maximum number of times.</summary>
    /// <param name="retryCount">int representing the retry count.</param>"
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class RetryAttribute : Attribute
    {
        private const int MIN_RETRY_COUNT = 1;
        private const int MAX_RETRY_COUNT = 10;

        public RetryAttribute(
            int retryCount)
        {
            if (retryCount < MIN_RETRY_COUNT || MAX_RETRY_COUNT < retryCount)
            {
                retryCount = MIN_RETRY_COUNT;
            }

            Value = retryCount;
        }

        public int Value { get; }
    }
}
