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
        [TestMethod()]
        [DataRow(UnitTestOutcome.Failed)]
        [DataRow(UnitTestOutcome.Inconclusive)]
        [DataRow(UnitTestOutcome.Passed)]
        [DataRow(UnitTestOutcome.InProgress)]
        [DataRow(UnitTestOutcome.Error)]
        [DataRow(UnitTestOutcome.Timeout)]
        [DataRow(UnitTestOutcome.Aborted)]
        [DataRow(UnitTestOutcome.Unknown)]
        [DataRow(UnitTestOutcome.NotRunnable)]
        public void BugAttributeDoesNotAlterExecutionForAllTestOutcomes(
            UnitTestOutcome RequiredTestOutCome)
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
                        new BugAttribute(177) // 177 is just some int representing a bug ID
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
            mockTestMethod.Verify(tm => tm.Invoke(args), Times.Once);
            Assert.AreEqual(tr.Length, 1);
            Assert.IsTrue((tr.All((r) => r.Outcome == RequiredTestOutCome)));
        }
    }
}
