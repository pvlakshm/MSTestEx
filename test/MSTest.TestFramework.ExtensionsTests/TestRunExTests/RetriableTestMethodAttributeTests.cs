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
            var mockPassingTestMethod = new Mock<ITestMethod>();
            var args = It.IsAny<object[]>();
            var passingTestResult = new TestResult() { Outcome = UnitTestOutcome.Passed };
            mockPassingTestMethod.Setup(tm => tm.Invoke(args)).Returns(passingTestResult);

            const int RETRYCOUNT = 5;
            var retriableTestMethod = new RetriableTestMethodAttribute(RETRYCOUNT);

            // Act
            var tr = retriableTestMethod.Execute(mockPassingTestMethod.Object);

            // Assert
            mockPassingTestMethod.Verify(tm => tm.Invoke(args), Times.Once);
            Assert.AreEqual(1, tr.Length);
            Assert.AreEqual(UnitTestOutcome.Passed, tr.First().Outcome);
        }

        [TestMethod]
        public void FailingTestShouldBeExecutedRetryCountNumberOfTimesAndReturnFailure()
        {
            // Arrange
            var mockFailingTestMethod = new Mock<ITestMethod>();
            var args = It.IsAny<object[]>();
            var failingTestResult = new TestResult() { Outcome = UnitTestOutcome.Failed };
            mockFailingTestMethod.Setup(tm => tm.Invoke(args)).Returns(failingTestResult);

            const int RETRYCOUNT = 5;
            var retriableTestMethod = new RetriableTestMethodAttribute(RETRYCOUNT);

            // Act
            var tr = retriableTestMethod.Execute(mockFailingTestMethod.Object);

            // Assert
            mockFailingTestMethod.Verify(tm => tm.Invoke(args), Times.Exactly(RETRYCOUNT));
            Assert.AreEqual(1, tr.Length);
            Assert.AreEqual(UnitTestOutcome.Failed, tr.First().Outcome);
        }

        [TestMethod]
        public void FlakyTestShouldBeExecutedtMultipleTimesTillItPasses()
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

            const int RETRYCOUNT = 5;
            var retriableTestMethod = new RetriableTestMethodAttribute(RETRYCOUNT);

            // Act
            var tr = retriableTestMethod.Execute(mockFlakyTestMethod.Object);

            // Assert
            mockFlakyTestMethod.Verify(tm => tm.Invoke(args), Times.Exactly(3));
            Assert.AreEqual(1, tr.Length);
            Assert.AreEqual(UnitTestOutcome.Passed, tr.First().Outcome);
        }

        [TestMethod]
        public void FlakyTestShouldBeExecutedtNoMoreThanRetyCountNumberOfTimes()
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

            const int RETRYCOUNT = 5;
            var retriableTestMethod = new RetriableTestMethodAttribute(RETRYCOUNT);

            // Act
            var tr = retriableTestMethod.Execute(mockFlakyTestMethod.Object);

            // Assert
            mockFlakyTestMethod.Verify(tm => tm.Invoke(args), Times.Exactly(RETRYCOUNT));
            Assert.AreEqual(1, tr.Length);
            Assert.AreEqual(UnitTestOutcome.Failed, tr.First().Outcome);
        }
    }
}
