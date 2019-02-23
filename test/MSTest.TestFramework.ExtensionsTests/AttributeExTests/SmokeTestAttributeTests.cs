using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.TestFramework.Extensions.AttributeEx;
using MSTest.TestFramework.Extensions.TestMethodEx;
using Moq;
using System.Linq;
using System;

namespace MSTest.TestFramework.ExtensionsTests.AttributeExTests
{
    [TestClass]
    public class SmokeTestAttributeTests
    {
        [TestMethod]
        public void TestMethodWithOneSmokeTestAttributeAndIsExecutedOnlyOnce()
        {
            // Arrange
            var mockPassingTestMethod = new Mock<ITestMethod>();

            var args = It.IsAny<object[]>();
            var passingTestResult = new TestResult() { Outcome = UnitTestOutcome.Passed };
            mockPassingTestMethod.Setup(tm => tm.Invoke(args)).Returns(passingTestResult);

            mockPassingTestMethod.Setup(tm => tm.GetAllAttributes(false)).Returns(() =>
                {
                    Attribute[] attr = {
                        new SmokeTestAttribute()
                    };
                    return attr;
                });

            var tmx = new TestMethodExAttribute();

            // Act
            var tr = tmx.Execute(mockPassingTestMethod.Object);

            // Assert
            mockPassingTestMethod.Verify(tm => tm.Invoke(args), Times.Once);
            Assert.AreEqual(1, tr.Length);
            Assert.AreEqual(UnitTestOutcome.Passed, tr.First().Outcome);
        }
    }
}
