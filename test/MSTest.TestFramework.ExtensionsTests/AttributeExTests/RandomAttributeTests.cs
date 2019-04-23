using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.TestFramework.Extensions.AttributeEx;
using MSTest.TestFramework.Extensions.TestMethodEx;

namespace MSTest.TestFramework.ExtensionsTests.AttributeExTests
{
    [TestClass]
    public class RandomAttributeTestsEx
    {
        [TestMethodEx]
        [RandomAttribute(2, 5)]
        public void addIntEx(int x, int y)
        {
            Assert.IsTrue(2 <= x && x < 5);
            Assert.IsTrue(2 <= y && y < 5);
        }

        [TestMethodEx]
        [RandomAttribute(1, 5)]
        public void addUintEx(uint x, uint y)
        {
            Assert.IsTrue(1 <= x && x < 5);
            Assert.IsTrue(1 <= y && y < 5);
        }

        [TestMethodEx]
        [RandomAttribute(1, 5)]
        public void addShortEx(short x, short y)
        {
            Assert.IsTrue(1 <= x && x < 5);
            Assert.IsTrue(1 <= y && y < 5);
        }

        [TestMethodEx]
        [RandomAttribute(1, 5)]
        public void addUshortEx(ushort x, ushort y)
        {
            Assert.IsTrue(1 <= x && x < 5);
            Assert.IsTrue(1 <= y && y < 5);
        }

        [TestMethodEx]
        [RandomAttribute(1, 5)]
        public void addByteEx(byte x, byte y)
        {
            Assert.IsTrue(1 <= x && x < 5);
            Assert.IsTrue(1 <= y && y < 5);
        }

        [TestMethodEx]
        [RandomAttribute(1, 5)]
        public void addLongEx(long x, long y)
        {
            Assert.IsTrue(1 <= x && x < 5);
            Assert.IsTrue(1 <= y && y < 5);
        }

        [TestMethodEx]
        [RandomAttribute(1, 5)]
        public void addUlongEx(ulong x, ulong y)
        {
            Assert.IsTrue(1 <= x && x < 5);
            Assert.IsTrue(1 <= y && y < 5);
        }

        [TestMethodEx]
        [RandomAttribute(1, 5)]
        public void addMultiTypesEx(
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

    [TestClass]
    public class RandomAttributeTests
    {
        [TestMethod]
        [RandomAttribute(2, 5)]
        public void addInt(int x, int y)
        {
            Assert.IsTrue(2 <= x && x < 5);
            Assert.IsTrue(2 <= y && y < 5);
        }

        [TestMethod]
        [RandomAttribute(1, 5)]
        public void addUint(uint x, uint y)
        {
            Assert.IsTrue(1 <= x && x < 5);
            Assert.IsTrue(1 <= y && y < 5);
        }

        [TestMethod]
        [RandomAttribute(1, 5)]
        public void addShort(short x, short y)
        {
            Assert.IsTrue(1 <= x && x < 5);
            Assert.IsTrue(1 <= y && y < 5);
        }

        [TestMethod]
        [RandomAttribute(1, 5)]
        public void addUshort(ushort x, ushort y)
        {
            Assert.IsTrue(1 <= x && x < 5);
            Assert.IsTrue(1 <= y && y < 5);
        }

        [TestMethod]
        [RandomAttribute(1, 5)]
        public void addByte(byte x, byte y)
        {
            Assert.IsTrue(1 <= x && x < 5);
            Assert.IsTrue(1 <= y && y < 5);
        }

        [TestMethod]
        [RandomAttribute(1, 5)]
        public void addLong(long x, long y)
        {
            Assert.IsTrue(1 <= x && x < 5);
            Assert.IsTrue(1 <= y && y < 5);
        }

        [TestMethod]
        [RandomAttribute(1, 5)]
        public void addUlong(ulong x, ulong y)
        {
            Assert.IsTrue(1 <= x && x < 5);
            Assert.IsTrue(1 <= y && y < 5);
        }

        [TestMethod]
        [RandomAttribute(1, 5)]
        public void addMultiTypes(
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