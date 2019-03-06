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
        [TestMethod()]
        [DataRow(UnitTestOutcome.Failed, 1, 1)]
        [DataRow(UnitTestOutcome.Failed, 5, 5)]
        [DataRow(UnitTestOutcome.Failed, 15, 1)]
        [DataRow(UnitTestOutcome.Inconclusive, 1, 1)]
        [DataRow(UnitTestOutcome.Inconclusive, 5, 1)]
        [DataRow(UnitTestOutcome.Inconclusive, 15, 1)]
        [DataRow(UnitTestOutcome.Passed, 1, 1)]
        [DataRow(UnitTestOutcome.Passed, 5, 1)]
        [DataRow(UnitTestOutcome.Passed, 15, 1)]
        [DataRow(UnitTestOutcome.InProgress, 1, 1)]
        [DataRow(UnitTestOutcome.InProgress, 5, 1)]
        [DataRow(UnitTestOutcome.InProgress, 15, 1)]
        [DataRow(UnitTestOutcome.Error, 1, 1)]
        [DataRow(UnitTestOutcome.Error, 5, 1)]
        [DataRow(UnitTestOutcome.Error, 15, 1)]
        [DataRow(UnitTestOutcome.Timeout, 1, 1)]
        [DataRow(UnitTestOutcome.Timeout, 5, 1)]
        [DataRow(UnitTestOutcome.Timeout, 15, 1)]
        [DataRow(UnitTestOutcome.Aborted, 1, 1)]
        [DataRow(UnitTestOutcome.Aborted, 5, 1)]
        [DataRow(UnitTestOutcome.Aborted, 15, 1)]
        [DataRow(UnitTestOutcome.Unknown, 1, 1)]
        [DataRow(UnitTestOutcome.Unknown, 5, 1)]
        [DataRow(UnitTestOutcome.Unknown, 15, 1)]
        [DataRow(UnitTestOutcome.NotRunnable, 1, 1)]
        [DataRow(UnitTestOutcome.NotRunnable, 5, 1)]
        [DataRow(UnitTestOutcome.NotRunnable, 15, 1)]
        public void RetryTestForAllTestOutcomes(
            UnitTestOutcome RequiredTestOutCome,
            int RequestedRetryCount,
            int expectedExecutionAttempts)
        {
            // Arrange
            TestResult[] expected =
                {
                    new TestResult() { Outcome = RequiredTestOutCome }
                };

            var mockTestMethod = new Mock<ITestMethod>();
            mockTestMethod.Setup(tm => tm.GetAllAttributes(false)).Returns(() =>
                {
                    Attribute[] attr =
                        {
                            new RetryAttribute(RequestedRetryCount),
                        };
                    return attr;
                }
            );

            var args = It.IsAny<object[]>();
            mockTestMethod.Setup(tm => tm.Invoke(args)).Returns(() =>
                {
                    return expected[0];
                }
            );


            // Act
            var retriableTestMethod = new TestMethodExAttribute();
            var tr = retriableTestMethod.Execute(mockTestMethod.Object);

            // Assert
            mockTestMethod.Verify(tm => tm.Invoke(args), Times.Exactly(expectedExecutionAttempts));
            Assert.AreEqual(tr.Length, expectedExecutionAttempts);
            Assert.IsTrue((tr.All((r) => r.Outcome == RequiredTestOutCome)));
        }
    }
}
