# TestMethodExAttribute
The __TestMethodExAttribute__ extends the TestMethodAttribute and enables custom execution.  
Other attributes like __RetryAttribute__ can then be added to control execution.

## Arguments
input - none.

## Usage
- add a NuGet reference to the MSTestEx package.
- create / open an MSTestV2 based test project.
- add the following:
```
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.TestFramework.Extensions.TestMethodEx;

namespace UnitTestProject2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethodEx]
        public void TestMethod1()
        {
            // Test logic
        }
    }
}
```

## Applies to
MSTestEx v1.0.2