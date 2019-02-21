# SmokeTestAttribute
The __SmokeTestAttribute__ marks a test method as a smoke test.
This now integrates with the filter / search experiences in the Visual Studio Test Explorer.

## Arguments
input - none.

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
		[SmokeTest]
        public void TestMethod1()
        {
            // Test logic
        }
    }
}
```

## Notes
Filtering by this attribute is not yet supported from vstest.console.exe.