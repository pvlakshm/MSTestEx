using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.TestFramework.Extensions.AttributeEx;

namespace MSTest.TestFramework.ExtensionsTests.AttributeExTests
{
    [TestClass]
    public class RandomAttributeTests
    {
        [TestMethod]
        [RandomAttribute(1, 2, 10)]
        public void add(int x, int y, int sum)
        {
            int val = x + y;
            Assert.AreEqual(sum, val);
        }
    }
}