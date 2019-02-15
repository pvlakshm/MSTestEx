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
            retriableTestMethod.Execute(mockTestMethod.Object);

            // Assert (using Moq's Verify)
            mockTestMethod.Verify(tm => tm.Invoke(args), Times.Once);
        }

        [TestMethod]
        public void ExecuteShouldRunTheTestMultipleTimesTillItPasses()
        {
            //int count = 0;
            //this.mockTestMethod.Setup(tm => tm.Invoke(It.IsAny<object[]>()))
            //    .Returns(() =>
            //    {
            //        count++;
            //        if (count < 3)
            //        {
            //            return new TestResult() { Outcome = UnitTestOutcome.Failed };
            //        }
            //        return new TestResult() { Outcome = UnitTestOutcome.Passed };
            //    });

            //this.retriableTestMethod.Execute(this.mockTestMethod.Object);
            //this.mockTestMethod.Verify(tm => tm.Invoke(It.IsAny<object[]>()), Times.Exactly(3));
        }

        [TestMethod]
        public void ExecuteShouldRunTheTestRetryCountNumberOfTimesAndReturnFailure()
        {
            //this.mockTestMethod.Setup(tm => tm.Invoke(It.IsAny<object[]>())).Returns(new TestResult() { Outcome = UnitTestOutcome.Failed });
            //var tr = this.retriableTestMethod.Execute(this.mockTestMethod.Object);
            //this.mockTestMethod.Verify(tm => tm.Invoke(It.IsAny<object[]>()), Times.Exactly(5));
            //Assert.AreEqual(UnitTestOutcome.Failed, tr.FirstOrDefault().Outcome);
        }
    }
}
