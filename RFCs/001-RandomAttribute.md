# RFC 001 - RandomAttribute

## Summary
- The __RandomAttribute__ can be used to specify a set of random values to be provided for an individual numeric parameter of a data driven test method.
- A ```min```, ```max``` range can be provided to constrain the values that are generated.
- A ```count``` can be specified to indicate the number of values to be provided.
- A ```distinct``` can be provided to have the randomizer generate distinct values. By default this will be set to ```false```.
- Each execution attempt will be recorded as a child test.
- As in the case of any data driven test, the execution attempts will be in sequence.

## Example
The following test will be executed 10 times.
```
    [TestClass]
    public class MyClass
    {
        [TestMethod]
        [RandomAttribute(1, 2, 10)]
        public void add(int x, int y)
        {
            // ...
        }
    }
```

The RandomAttribute will support the following contructors:
```
    public RandomAttribute(int min, int max, int count);
    public RandomAttribute(uint min, uint max, int count);
    public RandomAttribute(long min, long max, int count);
    public RandomAttribute(ulong min, ulong max, int count);
    public RandomAttribute(short min, short max, int count);
    public RandomAttribute(ushort min, ushort max, int count);
    public RandomAttribute(byte min, byte max, int count);
    public RandomAttribute(sbyte min, sbyte max, int count);
    public RandomAttribute(double min, double max, int count);
    public RandomAttribute(float min, float max, int count);
```