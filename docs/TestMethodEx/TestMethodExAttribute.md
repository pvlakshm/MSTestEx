# TestMethodExAttriute
input - none.

## Usage
- add a NuGet reference to the MSTestEx package.
- Create / open an MSTestV2 based test project
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
## Semantics
TestMethod1() has been annotated as a TestMethod**__Ex__**.
Attributes like Retry, etc. can now be used to control test execution.