# RandomAttribute
- The __RandomAttribute__ is used to specify a set of random values to be provided for an individual numeric parameter of a data driven test method.
- A ```min```, ```max``` range can be provided to constrain the values that are generated. These will be integer values.
- In the case where the parameter type is integral, an integral random value within the given range will be generated and cast to the parameter type.
- In the case where the parameter type is floating point, a double precision random value within the given range will be generated and cast to the parameter type.

## Arguments
min - int representing the inclsuive lower bound of the random number generated.
max - int representing the exclusive upper bound of the random number generated.

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
    public class MyClass
    {
        [TestMethodEx]
        [RandomAttribute(2, 5)]
        public void add(int x, int y)
        {
            // x, y will have a value >= 2 and < 5
        }

        [TestMethodEx]
        [RandomAttribute(2, 5)]
        public void add(byte x, int y, long z)
        {
            // x, y, z will have a value >= 2 and < 5
        }

        [TestMethodEx]
        [RandomAttribute(2, 5)]
        public void add(float x, double y)
        {
            // x, y will have a value >= 2.0 and < 5.0 (within floating point tolerances)
        }
    }
```

## Applies to
MSTestEx v1.0.4-preview
