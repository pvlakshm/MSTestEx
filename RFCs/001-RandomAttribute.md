# RFC 001 - RandomAttribute

## Summary
- The __RandomAttribute__ can be used to specify a set of random values to be provided for an individual numeric parameter of a data driven test method.
- A ```min```, ```max``` range can be provided to constrain the values that are generated. These will be integer values.
- In the case where the parameter type is integral, an integral random value within the given range will be generated and cast to the parameter type.
- In the case where the parameter type is floating point, a double precision random value within the given range will be generated and cast to the parameter type.
- The RandomAttribute will compose with the __RetryAttribute__, __RepeatAttribute__, and any other attributes supported on a ```[TestMethodEx]```.

Note
- This approach is a custom implementation of the ```ITestDataSource``` interface.
- This built-in implementation of RandomAttribute can serve as an example of how a set of random values can be provided for chars and strings, or any user defined types as well.

## Example
```
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
## Status
Implemented as #151, #152.  
Shipping in MSTestEx v1.0.4-preiview.