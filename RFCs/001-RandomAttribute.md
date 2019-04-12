# RFC 001 - RandomAttribute

## Summary
- The __RandomAttribute__ can be used to specify a set of random values to be provided for an individual numeric parameter of a data driven test method.
- A ```min```, ```max``` range can be provided to constrain the values that are generated. These will be integer values.
- A ```count``` can be specified to indicate the number of values to be provided. This will be an integer value, with the default set to 1.
- A ```distinct``` can be provided to have the randomizer generate distinct values. This will be a boolean value, with the default set to ```false```.
- Each execution attempt will be recorded as a child test.
- In the case where the parameter type is integral, an integral random value is generated and cast to the parameter type.
- In the case where the parameter type is double or float, a double precision random value is generated and cast to the parameter type.

Note
- As in the case of any data driven test, the execution attempts will be in sequence.
- This approach is a custom implementation of the ```ITestDataSource``` interface.
- Consequently, the attribute can be used only on a ```[TestMethod]```.
- The built-in implementation of RandomAttribute can serve as an example of how a a set of random values can be provided for chars and strings, or any combination of parameter types as well.

## Example
```
    [TestClass]
    public class MyClass
    {
        [TestMethod]
        [RandomAttribute(2, 5)]
        public void add(int x, int y)
        {
            // x, y will have a value >= 2 and < 5
        }

        [TestMethod]
        [RandomAttribute(2, 5, 5)]
        public void add(int x, int y)
        {
            // x, y will have a value >= 2 and < 5
            // the test will be execute 5 times.
        }

        [TestMethod]
        [RandomAttribute(2, 5, 5)]
        public void add(byte x, int y, long z)
        {
            // x, y, z will have a value >= 2 and < 5
            // the test will be execute 5 times.
        }

        [TestMethod]
        [RandomAttribute(2, 5, 5)]
        public void add(float x, double y)
        {
            // x, y will have a value >= 2.0 and < 5.0 (within floating point tolerances)
            // the test will be execute 5 times.
        }
    }
```
