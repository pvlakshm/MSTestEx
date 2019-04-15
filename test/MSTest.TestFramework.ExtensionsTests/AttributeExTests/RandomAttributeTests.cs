using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.TestFramework.Extensions.AttributeEx;
using MSTest.TestFramework.Extensions.TestMethodEx;

namespace MSTest.TestFramework.ExtensionsTests.AttributeExTests
{
    [TestClass]
    public class IntRandomAttributeTests
    {
        [TestMethodEx]
        [RandomAttribute(2, 5)]
        public void add(int x, int y)
        {
            Assert.IsTrue(2 <= x && x < 5);
            Assert.IsTrue(2 <= y && y < 5);
        }

        [TestMethodEx]
        [RandomAttribute(1, 5)]
        public void add2(uint x, uint y)
        {
            Assert.IsTrue(1 <= x && x < 5);
            Assert.IsTrue(1 <= y && y < 5);
        }

        [TestMethodEx]
        [RandomAttribute(1, 5)]
        public void add3(short x, short y)
        {
            Assert.IsTrue(1 <= x && x < 5);
            Assert.IsTrue(1 <= y && y < 5);
        }

        [TestMethodEx]
        [RandomAttribute(1, 5)]
        public void add4(ushort x, ushort y)
        {
            Assert.IsTrue(1 <= x && x < 5);
            Assert.IsTrue(1 <= y && y < 5);
        }

        [TestMethodEx]
        [RandomAttribute(1, 5)]
        public void add5(byte x, byte y)
        {
            Assert.IsTrue(1 <= x && x < 5);
            Assert.IsTrue(1 <= y && y < 5);
        }

        [TestMethodEx]
        [RandomAttribute(1, 5)]
        public void add6(long x, long y)
        {
            Assert.IsTrue(1 <= x && x < 5);
            Assert.IsTrue(1 <= y && y < 5);
        }

        [TestMethodEx]
        [RandomAttribute(1, 5)]
        public void add7(ulong x, ulong y)
        {
            Assert.IsTrue(1 <= x && x < 5);
            Assert.IsTrue(1 <= y && y < 5);
        }

        [TestMethodEx]
        [RandomAttribute(1, 5)]
        public void add8(
            int x,
            uint y,
            short s,
            ushort us,
            byte b,
            long l,
            ulong ul,
            float f,
            double d)
        {
            Assert.IsTrue(1 <= x && x < 5);
            Assert.IsTrue(1 <= y && y < 5);
            Assert.IsTrue(1 <= s && s < 5);
            Assert.IsTrue(1 <= us && us < 5);
            Assert.IsTrue(1 <= b && b < 5);
            Assert.IsTrue(1 <= l && l < 5);
            Assert.IsTrue(1 <= ul && ul < 5);
            Assert.IsTrue(1 <= f && f < 5);
            Assert.IsTrue(1 <= d && d < 5);
        }
    }
}