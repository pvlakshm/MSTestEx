using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.TestFramework.Extensions.TestRunEx;
using Moq;
using System.Linq;

namespace MSTest.TestFramework.ExtensionsTests.TestRunExTests
{
    [TestClass]
    public class RetriableTestMethodAttributeTests
    {
        [TestMethod]
        public void PassingTestShouldGetExecutedOnlyOnce()
        {
            // Arrange
            var mockTestMethod = new Mock<ITestMethod>();
            var args = It.IsAny<object[]>();
            var passingTestResult = new TestResult() { Outcome = UnitTestOutcome.Passed };
            mockTestMethod.Setup(tm => tm.Invoke(args)).Returns(passingTestResult);

            const int retryCount = 5;
            var retriableTestMethod = new RetriableTestMethodAttribute(retryCount);

            // Act
            var tr = retriableTestMethod.Execute(mockTestMethod.Object);

            // Assert
            mockTestMethod.Verify(tm => tm.Invoke(args), Times.Once);
            Assert.AreEqual(1, tr.Length);
            Assert.AreEqual(UnitTestOutcome.Passed, tr.First().Outcome);
        }

        [TestMethod]
        public void FlakyTestShouldBeExecutedtMultipleTimesTillItPasses()
        {
            // Arrange
            var mockTestMethod = new Mock<ITestMethod>();
            var args = It.IsAny<object[]>();
            var passingTestResult = new TestResult() { Outcome = UnitTestOutcome.Passed };
            var failingTestResult = new TestResult() { Outcome = UnitTestOutcome.Failed };

            int count = 0;
            mockTestMethod.Setup(tm => tm.Invoke(args)).Returns(() =>
                {
                    count++;
                    if (count < 3)
                    {
                        return failingTestResult;
                    }
                    return passingTestResult;
                }
            );

            const int retryCount = 5;
            var retriableTestMethod = new RetriableTestMethodAttribute(retryCount);

            // Act
            var tr = retriableTestMethod.Execute(mockTestMethod.Object);

            // Assert
            mockTestMethod.Verify(tm => tm.Invoke(args), Times.Exactly(3));
            Assert.AreEqual(1, tr.Length);
            Assert.AreEqual(UnitTestOutcome.Passed, tr.First().Outcome);
        }

        [TestMethod]
        public void FailingTestShouldBeExecutedRetryCountNumberOfTimesAndReturnFailure()
        {
            // Arrange
            var mockTestMethod = new Mock<ITestMethod>();
            var args = It.IsAny<object[]>();
            var failingTestResult = new TestResult() { Outcome = UnitTestOutcome.Failed };
            mockTestMethod.Setup(tm => tm.Invoke(args)).Returns(failingTestResult);

            const int retryCount = 5;
            var retriableTestMethod = new RetriableTestMethodAttribute(retryCount);

            // Act
            var tr = retriableTestMethod.Execute(mockTestMethod.Object);

            // Assert
            mockTestMethod.Verify(tm => tm.Invoke(args), Times.Exactly(retryCount));
            Assert.AreEqual(1, tr.Length);
            Assert.AreEqual(UnitTestOutcome.Failed, tr.First().Outcome);
        }
    }
}
