using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.TestFramework.Extensions.AttributeEx;
using MSTest.TestFramework.Extensions.TestMethodEx;
using Moq;
using System.Linq;
using System;

namespace MSTest.TestFramework.ExtensionsTests.AttributeExTests
{
    [TestClass]
    public class RetryAttributeTests
    {
        [TestMethod]
        public void Ex_PassingTestShouldGetExecutedOnlyOnce()
        {
            // Arrange
            var mockPassingTestMethod = new Mock<ITestMethod>();

            var args = It.IsAny<object[]>();
            var passingTestResult = new TestResult() { Outcome = UnitTestOutcome.Passed };
            mockPassingTestMethod.Setup(tm => tm.Invoke(args)).Returns(passingTestResult);

            const int RETRY_COUNT = 5;
            mockPassingTestMethod.Setup(tm => tm.GetAllAttributes(false)).Returns(() =>
                {
                    Attribute[] attr = { new RetryAttribute(RETRY_COUNT) };
                    return attr;
                });

            var retriableTestMethod = new TestMethodExAttribute();

            // Act
            var tr = retriableTestMethod.Execute(mockPassingTestMethod.Object);

            // Assert
            mockPassingTestMethod.Verify(tm => tm.Invoke(args), Times.Once);
            Assert.AreEqual(1, tr.Length);
            Assert.AreEqual(UnitTestOutcome.Passed, tr.First().Outcome);
        }

        [TestMethod]
        public void Ex_FailingTestShouldBeExecutedRetryCountNumberOfTimesAndReturnFailure()
        {
            // Arrange
            var mockPassingTestMethod = new Mock<ITestMethod>();

            var args = It.IsAny<object[]>();
            var failingTestResult = new TestResult() { Outcome = UnitTestOutcome.Failed };
            mockPassingTestMethod.Setup(tm => tm.Invoke(args)).Returns(failingTestResult);

            const int RETRY_COUNT = 5;
            mockPassingTestMethod.Setup(tm => tm.GetAllAttributes(false)).Returns(() =>
            {
                Attribute[] attr = { new RetryAttribute(RETRY_COUNT) };
                return attr;
            });

            var retriableTestMethod = new TestMethodExAttribute();

            // Act
            var tr = retriableTestMethod.Execute(mockPassingTestMethod.Object);

            // Assert
            mockPassingTestMethod.Verify(tm => tm.Invoke(args), Times.Exactly(RETRY_COUNT));
            Assert.AreEqual(1, tr.Length);
            Assert.AreEqual(UnitTestOutcome.Failed, tr.First().Outcome);
        }

        [TestMethodEx]
        public void Ex_FlakyTestShouldBeExecutedtMultipleTimesTillItPasses()
        {
            // Arrange
            var mockFlakyTestMethod = new Mock<ITestMethod>();
            var args = It.IsAny<object[]>();
            var passingTestResult = new TestResult() { Outcome = UnitTestOutcome.Passed };
            var failingTestResult = new TestResult() { Outcome = UnitTestOutcome.Failed };

            int count = 0;
            const int TRIGGER = 3;
            mockFlakyTestMethod.Setup(tm => tm.Invoke(args)).Returns(() =>
            {
                count++;
                if (count < TRIGGER)
                {
                    return failingTestResult;
                }
                return passingTestResult;
            }
            );

            const int RETRY_COUNT = 5;
            mockFlakyTestMethod.Setup(tm => tm.GetAllAttributes(false)).Returns(() =>
            {
                Attribute[] attr = { new RetryAttribute(RETRY_COUNT) };
                return attr;
            });
            var retriableTestMethod = new TestMethodExAttribute();

            // Act
            var tr = retriableTestMethod.Execute(mockFlakyTestMethod.Object);

            // Assert
            mockFlakyTestMethod.Verify(tm => tm.Invoke(args), Times.Exactly(TRIGGER));
            Assert.AreEqual(1, tr.Length);
            Assert.AreEqual(UnitTestOutcome.Passed, tr.First().Outcome);
        }

        [TestMethod]
        public void Ex_FlakyTestShouldBeExecutedtNoMoreThanRetryCountNumberOfTimes()
        {
            // Arrange
            var mockFlakyTestMethod = new Mock<ITestMethod>();
            var args = It.IsAny<object[]>();
            var passingTestResult = new TestResult() { Outcome = UnitTestOutcome.Passed };
            var failingTestResult = new TestResult() { Outcome = UnitTestOutcome.Failed };

            int count = 0;
            const int TRIGGER = 8;
            mockFlakyTestMethod.Setup(tm => tm.Invoke(args)).Returns(() =>
            {
                count++;
                if (count < TRIGGER)
                {
                    return failingTestResult;
                }
                return passingTestResult;
            }
            );

            const int RETRY_COUNT = 5;
            mockFlakyTestMethod.Setup(tm => tm.GetAllAttributes(false)).Returns(() =>
            {
                Attribute[] attr = { new RetryAttribute(RETRY_COUNT) };
                return attr;
            });
            var retriableTestMethod = new TestMethodExAttribute();

            // Act
            var tr = retriableTestMethod.Execute(mockFlakyTestMethod.Object);

            // Assert
            mockFlakyTestMethod.Verify(tm => tm.Invoke(args), Times.Exactly(RETRY_COUNT));
            Assert.AreEqual(1, tr.Length);
            Assert.AreEqual(UnitTestOutcome.Failed, tr.First().Outcome);
        }
    }
}
