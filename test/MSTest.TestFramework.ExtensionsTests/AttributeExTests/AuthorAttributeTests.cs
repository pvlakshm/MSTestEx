using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.TestFramework.Extensions.AttributeEx;
using MSTest.TestFramework.Extensions.TestMethodEx;
using Moq;
using System.Linq;
using System;

namespace MSTest.TestFramework.ExtensionsTests.AttributeExTests
{
    [TestClass]
    public class AuthorAttributeTests
    {
        [TestMethod]
        public void TestCanHaveSingleAuthorAttributeAndIsExecutedOnlyOnce()
        {
            // Arrange
            var mockPassingTestMethod = new Mock<ITestMethod>();

            var args = It.IsAny<object[]>();
            var passingTestResult = new TestResult() { Outcome = UnitTestOutcome.Passed };
            mockPassingTestMethod.Setup(tm => tm.Invoke(args)).Returns(passingTestResult);

            mockPassingTestMethod.Setup(tm => tm.GetAllAttributes(false)).Returns(() =>
                {
                    Attribute[] attr = {
                        new AuthorAttribute("John Doe")
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

        [TestMethod]
        public void TestCanHaveMultipleAuthorAttributeAndIsExecutedOnlyOnce()
        {
            // Arrange
            var mockPassingTestMethod = new Mock<ITestMethod>();

            var args = It.IsAny<object[]>();
            var passingTestResult = new TestResult() { Outcome = UnitTestOutcome.Passed };
            mockPassingTestMethod.Setup(tm => tm.Invoke(args)).Returns(passingTestResult);

            mockPassingTestMethod.Setup(tm => tm.GetAllAttributes(false)).Returns(() =>
            {
                Attribute[] attr = {
                    new AuthorAttribute("John Doe"),
                    new AuthorAttribute("Joe Engineer")
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
