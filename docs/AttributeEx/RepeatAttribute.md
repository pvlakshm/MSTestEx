# RepeatAttribute
- The __RepeatAttribute__ is used on a test method to specify that it should be rerun if it passes, up to a maximum number of times.
- Only tests with an outcome of "Passed", can trigger a repeat. For the complete set of possible test outcomes see [here](https://github.com/Microsoft/testfx/blob/master/src/TestFramework/MSTest.Core/UnitTestOutcome.cs).
- A test method can be repeated up to a maximum of 50 times (the input argument representing the retry count will be automatically sanitized to be within the range 1 to 50).
- Each execution attempt is recorded as a child test.
- In the case of data driven tests all invocations of the test method must pass to trigger a repeat.

## Arguments
input - int representing the repeat count.

## Usage
- add a NuGet reference to the MSTestEx package.
- create / open an MSTestV2 based test project.
- add the following:
```
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.TestFramework.Extensions.AttributeEx;
using MSTest.TestFramework.Extensions.TestMethodEx;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethodEx]
        [Repeat(5)]
        public void TestMethod1()
        {
            // Test logic that causes the test to pass.
        }
    }
}
```

## Notes
 - Note that any surrounding TestInitialize and TestCleanup methods will be executed only once.