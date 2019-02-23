using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.TestFramework.Extensions.AttributeEx;
using MSTest.TestFramework.Extensions.TestMethodEx;
using Moq;
using System.Linq;
using System;

namespace MSTest.TestFramework.ExtensionsTests.AttributeExTests
{
    [TestClass]
    public class BugAttributeTests
    {
        [TestMethod]
        public void TestMethodWithOneBugAttributeIsExecutedOnlyOnce()
        {
            // Arrange
            var mockPassingTestMethod = new Mock<ITestMethod>();

            var args = It.IsAny<object[]>();
            var passingTestResult = new TestResult() { Outcome = UnitTestOutcome.Passed };
            mockPassingTestMethod.Setup(tm => tm.Invoke(args)).Returns(passingTestResult);

            mockPassingTestMethod.Setup(tm => tm.GetAllAttributes(false)).Returns(() =>
                {
                    Attribute[] attr = {
                        new BugAttribute(177) // 177 is just some int representing a bug ID
                    };
                    return attr;
                });

            var tmx = new TestMethodExAttribute();

            // Act
            var tr = tmx.Execute(mockPassingTestMethod.Object);

            // Assert
            mockPassingTestMethod.Verify(tm => tm.Invoke(args), Times.Once);
            Assert.AreEqual(1, tr.Length);
            Assert.AreEqual(UnitTestOutcome.Passed, tr[0].Outcome);
        }

        [TestMethod]
        public void TestMethodWithMultipleBugAttributeIsExecutedOnlyOnce()
        {
            // Arrange
            var mockPassingTestMethod = new Mock<ITestMethod>();

            var args = It.IsAny<object[]>();
            var passingTestResult = new TestResult() { Outcome = UnitTestOutcome.Passed };
            mockPassingTestMethod.Setup(tm => tm.Invoke(args)).Returns(passingTestResult);

            mockPassingTestMethod.Setup(tm => tm.GetAllAttributes(false)).Returns(() =>
            {
                Attribute[] attr = {
                    new BugAttribute(177),  // 177, 277 are just some ints representing Bug IDs.
                    new BugAttribute(277),
                };
                return attr;
            });

            var tmx = new TestMethodExAttribute();

            // Act
            var tr = tmx.Execute(mockPassingTestMethod.Object);

            // Assert
            mockPassingTestMethod.Verify(tm => tm.Invoke(args), Times.Once);
            Assert.AreEqual(1, tr.Length);
            Assert.AreEqual(UnitTestOutcome.Passed, tr[0].Outcome);
        }
    }
}
