# AuthorAttribute
The __AuthorAttribute__ adds information about the author of a test method.  
This now integrates with the filter / search experiences in the Visual Studio Test Explorer.

## Arguments
input - string representing the author name.

## Usage
- add a NuGet reference to the MSTestEx package.
- create / open an MSTestV2 based test project.
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
        [Author("John Doe")]
        public void TestMethod1()
        {
            // Test logic
        }
    }
}
```

## Applies to
MSTestEx v1.0.2

## Notes
Filtering by this attribute is not yet supported from vstest.console.exe.