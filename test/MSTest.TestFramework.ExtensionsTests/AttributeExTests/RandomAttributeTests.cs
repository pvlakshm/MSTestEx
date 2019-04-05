using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.TestFramework.Extensions.AttributeEx;

namespace MSTest.TestFramework.ExtensionsTests.AttributeExTests
{
    [TestClass]
    public class RandomAttributeTests
    {
        [TestMethod]
        [RandomAttribute(1, 2, 10)]
        public void add(int x, int y)
        {
            Assert.IsTrue(1 <= x && x <= 2);
            Assert.IsTrue(1 <= y && y <= 2);
        }
    }
}