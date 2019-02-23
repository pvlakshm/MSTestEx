using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.TestFramework.Extensions.AttributeEx;
using MSTest.TestFramework.Extensions.TestMethodEx;
using Moq;
using System;

namespace MSTest.TestFramework.ExtensionsTests.AttributeExTests
{
    [TestClass]
    public class RetryAttributeTests
    {
        [TestMethod()]
        public void TestMethodWithExpectedExceptionExecutedRetryCountNumberOfTimes()
        {
            // Arrange
            var mockTestMethodWithException = new Mock<ITestMethod>();

            const int RETRY_COUNT = 5;
            mockTestMethodWithException.Setup(tm => tm.GetAllAttributes(false)).Returns(() =>
                {
                    Attribute[] attr = {
                        new RetryAttribute(RETRY_COUNT),
                        new ExpectedExceptionAttribute(typeof(System.DivideByZeroException))
                    };
                    return attr;
                }
            );

            var args = It.IsAny<object[]>();
            mockTestMethodWithException.Setup(tm => tm.Invoke(args)).Returns(() =>
                {
                    int numerator = 1;
                    int denominator = 0;
                    int val = numerator / denominator; // raise a DivideByZeroException
                    return new TestResult() { Outcome = UnitTestOutcome.Passed };
                }
            );

            var retriableTestMethod = new TestMethodExAttribute();

            // Act
            var tr = retriableTestMethod.Execute(mockTestMethodWithException.Object);

            // Assert
            mockTestMethodWithException.Verify(tm => tm.Invoke(args), Times.Exactly(RETRY_COUNT));
            Assert.AreEqual(0, tr.Length);
        }

        [TestMethod]
        public void TestMethodWithExceptionExecutedOnlyOnce()
        {
            // Arrange
            var mockTestMethodWithException = new Mock<ITestMethod>();

            const int RETRY_COUNT = 5;
            mockTestMethodWithException.Setup(tm => tm.GetAllAttributes(false)).Returns(() =>
                {
                    Attribute[] attr = { new RetryAttribute(RETRY_COUNT) };
                    return attr;
                }
            );

            var args = It.IsAny<object[]>();
            mockTestMethodWithException.Setup(tm => tm.Invoke(args)).Returns(() =>
                {
                    throw new Exception();
                }
            );

            var retriableTestMethod = new TestMethodExAttribute();

            // Act
            var tr = retriableTestMethod.Execute(mockTestMethodWithException.Object);

            // Assert
            mockTestMethodWithException.Verify(tm => tm.Invoke(args), Times.Once);
            Assert.AreEqual(0, tr.Length);
        }

        [TestMethod]
        public void TestMethodWithUnexpectedExceptionExecutedOnlyOnce()
        {
            // Arrange
            var mockTestMethodWithException = new Mock<ITestMethod>();

            const int RETRY_COUNT = 5;
            mockTestMethodWithException.Setup(tm => tm.GetAllAttributes(false)).Returns(() =>
                {
                    Attribute[] attr = {
                        new RetryAttribute(RETRY_COUNT),
                        new ExpectedExceptionAttribute(typeof(System.NullReferenceException))
                    };
                    return attr;
                }
            );

            var args = It.IsAny<object[]>();
            mockTestMethodWithException.Setup(tm => tm.Invoke(args)).Returns(() =>
            {
                throw new Exception();
            }
            );

            var retriableTestMethod = new TestMethodExAttribute();

            // Act
            var tr = retriableTestMethod.Execute(mockTestMethodWithException.Object);

            // Assert
            mockTestMethodWithException.Verify(tm => tm.Invoke(args), Times.Once);
            Assert.AreEqual(0, tr.Length);
        }

        [TestMethod]
        public void PassingTestMethodExecutedOnlyOnceAndReturnsPassed()
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
                }
            );

            var retriableTestMethod = new TestMethodExAttribute();

            // Act
            var tr = retriableTestMethod.Execute(mockPassingTestMethod.Object);

            // Assert
            mockPassingTestMethod.Verify(tm => tm.Invoke(args), Times.Once);
            Assert.AreEqual(1, tr.Length);
            Assert.AreEqual(UnitTestOutcome.Passed, tr[0].Outcome);
        }

        [TestMethod]
        public void FailingTestMethodExecutedRetryCountNumberOfTimesAndReturnsFailed()
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
                }
            );

            var retriableTestMethod = new TestMethodExAttribute();

            // Act
            var tr = retriableTestMethod.Execute(mockPassingTestMethod.Object);

            // Assert
            mockPassingTestMethod.Verify(tm => tm.Invoke(args), Times.Exactly(RETRY_COUNT));
            Assert.AreEqual(RETRY_COUNT, tr.Length);
            for (int i = 0; i < RETRY_COUNT; i++)
            {
                Assert.AreEqual(UnitTestOutcome.Failed, tr[i].Outcome);
            }
        }

        [TestMethodEx]
        public void FlakyPassingTestMethodExecutedtMultipleTimesAndReturnsPassed()
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
                }
            );
            var retriableTestMethod = new TestMethodExAttribute();

            // Act
            var tr = retriableTestMethod.Execute(mockFlakyTestMethod.Object);

            // Assert
            mockFlakyTestMethod.Verify(tm => tm.Invoke(args), Times.Exactly(TRIGGER));
            Assert.AreEqual(TRIGGER, tr.Length);
            int i = 0;
            for (i = 0; i < TRIGGER-1; i++)
            {
                Assert.AreEqual(UnitTestOutcome.Failed, tr[i].Outcome);
            }
            Assert.AreEqual(UnitTestOutcome.Passed, tr[i].Outcome);
        }

        [TestMethod]
        public void FlakyTestMethodExecutedtNoMoreThanRetryCountNumberOfTimesAndReturnsFailed()
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
                }
            );
            var retriableTestMethod = new TestMethodExAttribute();

            // Act
            var tr = retriableTestMethod.Execute(mockFlakyTestMethod.Object);

            // Assert
            mockFlakyTestMethod.Verify(tm => tm.Invoke(args), Times.Exactly(RETRY_COUNT));
            Assert.AreEqual(RETRY_COUNT, tr.Length);
            for (int i = 0; i < RETRY_COUNT; i++)
            {
                Assert.AreEqual(UnitTestOutcome.Failed, tr[i].Outcome);
            }
        }
    }
}
