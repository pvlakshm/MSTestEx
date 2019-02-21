# RetryAttribyute
input - int representing the retry count.

## Usage
- add a NuGet reference to the MSTestEx package.
- Create / open an MSTestV2 based test project
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
            // Test logic
        }
    }
}
```
## Semantics
TestMethod1() has been annotated as a retriable test method, with a retry count of 5. Each execution of this test method will be retried up to a maximum of 5 times. If any of those tries return a successful test outcome, no further tries will be made, and the test will be treated as a passing test.

## Notes
 - Note that any surrounding TestInitialize and TestCleanup methods will be executed only once.
 - If a test has an unexpected exception, it is not retried. Only assertion failures or ExpectedExceptions can trigger a retry. 