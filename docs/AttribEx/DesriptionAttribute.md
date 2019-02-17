# DescriptionAttriute
input - string representing a description.

## Usage
- add a NuGet reference to the MSTestEx package.
- Create / open an MSTestV2 based test project
- add the following:
```
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.TestFramework.Extensions.AttribEx;

namespace UnitTestProject2
{
    [TestClass]
    public class UnitTest1
    {
		[TestMethod]
        [Description("CPU intensive")]
        public void TestMethod1()
        {
            // Test logic
        }
    }
}
```
## Semantics
TestMethod1() has been annotated with a description.
This now integrates with the filter / search experiences in the Visual Studio Test Explorer.

## Notes
Filtering by this attribute is not yet supported from vstest.console.exe.