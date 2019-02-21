# BugAttriute
The __BugAttribute__ add a bug ID to a test method.  
This now integrates with the filter / search experiences in the Visual Studio Test Explorer.

## Arguments
input - int representing the bug ID.

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
        [Bug(101)]
        public void TestMethod1()
        {
            // Test logic
        }
    }
}
```

## Notes
Filtering by this attribute is not yet supported from vstest.console.exe.