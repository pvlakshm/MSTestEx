# RetryAttribute
- The __RetryAttribute__ is used on a test method to specify that it should be rerun if it fails, up to a maximum number of times.
- If the test has an unexpected exception, it is not retried. Only assertion failures can trigger a retry.
- A test method can be retried up to a maximum of 10 times (the input argument representing the retry count will be automatically sanitized to be within the range 1 to 10).
- Each execution attempt is recorded as a child test.

## Arguments
input - int representing the retry count.

## Usage
- add a NuGet reference to the MSTestEx package.
- create / open an MSTestV2 based test project.
- add the following:
```
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.TestFramework.Extensions.AttributeEx;

namespace UnitTestProject2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethodEx]
        [Retry(5)]
        public void TestMethod1()
        {
            // Test logic that causes the test to fail.
        }
    }
}
```

## Notes
 - Note that any surrounding TestInitialize and TestCleanup methods will be executed only once.