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
        [TestMethodEx]
        [Retry(2)]
        public void PassingTest()
        {
            Assert.IsTrue(true);
        }

        //[TestMethodEx]
        //[Retry(2)]
        //public void FailingTest()
        //{
        //    Assert.IsTrue(false);
        //}

        //[TestMethodEx]
        //[Ignore]
        //[Retry(2)]
        //public void IgnoredTest()
        //{
        //}

        //[TestMethodEx]
        //[Retry(2)]
        //public void InconclusiveTest()
        //{
        //    Assert.Inconclusive();
        //}

        //[TestMethodEx]
        //[Timeout(10)]
        //public void LongRunningTest()
        //{
        //    System.Threading.Thread.Sleep(1000 * 11);
        //}

        [TestMethodEx]
        [ExpectedException(typeof(System.DivideByZeroException))]
        [Retry(2)]
        public void PassingTestWithExpectedException()
        {
            int num = 1;
            int denom = 0;
            int val = num / denom;
        }

        //[TestMethodEx]
        //[Retry(2)]
        //public void FailingTestWithException()
        //{
        //    int num = 1;
        //    int denom = 0;
        //    int val = num / denom;
        //}




        //[TestMethodEx]
        //[ExpectedException(typeof(System.DivideByZeroException))]
        //[Retry(5)]
        //public void U_TestWithExpectedException_ExecutedOne_ReturnsPassed()
        //{
        //    int numerator = 1;
        //    int denominator = 0;
        //    int val = numerator / denominator; // raise a DivideByZeroException
        //}

        //[TestMethod()]
        //public void TestWithExpectedException_ExecutedOnce_ReturnsPassed()
        //{
        //    // Arrange
        //    var mockTestMethodWithException = new Mock<ITestMethod>();
        //    TestResult[] expected =
        //        {
        //            new TestResult() { Outcome = UnitTestOutcome.Passed }
        //        };

        //    const int RETRY_COUNT = 5;
        //    mockTestMethodWithException.Setup(tm => tm.GetAllAttributes(false)).Returns(() =>
        //        {
        //            Attribute[] attr = {
        //                new RetryAttribute(RETRY_COUNT),
        //                new ExpectedExceptionAttribute(typeof(System.DivideByZeroException))
        //            };
        //            return attr;
        //        }
        //    );

        //    var args = It.IsAny<object[]>();
        //    mockTestMethodWithException.Setup(tm => tm.Invoke(args)).Returns(() =>
        //        {
        //            int numerator = 1;
        //            int denominator = 0;
        //            int val = numerator / denominator; // raise a DivideByZeroException
        //            return new TestResult() { Outcome = UnitTestOutcome.Passed };
        //        }
        //    );

        //    var retriableTestMethod = new TestMethodExAttribute();

        //    // Act
        //    var tr = retriableTestMethod.Execute(mockTestMethodWithException.Object);

        //    // Assert
        //    mockTestMethodWithException.Verify(tm => tm.Invoke(args), Times.Once);
        //    CollectionAssert.AreEqual(expected, tr);
        //}

        //[TestMethod]
        //public void TestWithException_ExecutedOnce_ReturnsNoResult()
        //{
        //    // Arrange
        //    var mockTestMethodWithException = new Mock<ITestMethod>();
        //    TestResult[] expected = { };

        //    const int RETRY_COUNT = 5;
        //    mockTestMethodWithException.Setup(tm => tm.GetAllAttributes(false)).Returns(() =>
        //        {
        //            Attribute[] attr = { new RetryAttribute(RETRY_COUNT) };
        //            return attr;
        //        }
        //    );

        //    var args = It.IsAny<object[]>();
        //    mockTestMethodWithException.Setup(tm => tm.Invoke(args)).Returns(() =>
        //        {
        //            throw new Exception();
        //        }
        //    );

        //    var retriableTestMethod = new TestMethodExAttribute();

        //    // Act
        //    var tr = retriableTestMethod.Execute(mockTestMethodWithException.Object);

        //    // Assert
        //    mockTestMethodWithException.Verify(tm => tm.Invoke(args), Times.Once);
        //    CollectionAssert.AreEqual(expected, tr);
        //}

        //[TestMethod]
        //public void TestWithUnexpectedException_ExecutedOnce__ReturnsNoResult()
        //{
        //    // Arrange
        //    var mockTestMethodWithException = new Mock<ITestMethod>();
        //    TestResult[] expected = { };

        //    const int RETRY_COUNT = 5;
        //    mockTestMethodWithException.Setup(tm => tm.GetAllAttributes(false)).Returns(() =>
        //        {
        //            Attribute[] attr = {
        //                new RetryAttribute(RETRY_COUNT),
        //                new ExpectedExceptionAttribute(typeof(System.NullReferenceException))
        //            };
        //            return attr;
        //        }
        //    );

        //    var args = It.IsAny<object[]>();
        //    mockTestMethodWithException.Setup(tm => tm.Invoke(args)).Returns(() =>
        //        {
        //            throw new Exception();
        //        }
        //    );

        //    var retriableTestMethod = new TestMethodExAttribute();

        //    // Act
        //    var tr = retriableTestMethod.Execute(mockTestMethodWithException.Object);

        //    // Assert
        //    mockTestMethodWithException.Verify(tm => tm.Invoke(args), Times.Once);
        //    CollectionAssert.AreEqual(expected, tr);
        //}

        //[TestMethod]
        //public void PassingTest_ExecutedOnce_ReturnsPassed()
        //{
        //    // Arrange
        //    var mockPassingTestMethod = new Mock<ITestMethod>();
        //    TestResult[] expected =
        //        {
        //            new TestResult() { Outcome = UnitTestOutcome.Passed}
        //        };

        //    var args = It.IsAny<object[]>();
        //    mockPassingTestMethod.Setup(tm => tm.Invoke(args)).Returns(expected[0]);

        //    const int RETRY_COUNT = 5;
        //    mockPassingTestMethod.Setup(tm => tm.GetAllAttributes(false)).Returns(() =>
        //        {
        //            Attribute[] attr = { new RetryAttribute(RETRY_COUNT) };
        //            return attr;
        //        }
        //    );

        //    var retriableTestMethod = new TestMethodExAttribute();

        //    // Act
        //    var tr = retriableTestMethod.Execute(mockPassingTestMethod.Object);

        //    // Assert
        //    mockPassingTestMethod.Verify(tm => tm.Invoke(args), Times.Once);
        //    CollectionAssert.AreEqual(expected, tr);
        //}

        //[TestMethod]
        //public void FailingTest_ExecutedRetryCountNumberOfTimes_ReturnsFailed()
        //{
        //    // Arrange
        //    var mockPassingTestMethod = new Mock<ITestMethod>();
        //    TestResult[] expected =
        //        {
        //            new TestResult() { Outcome = UnitTestOutcome.Failed},
        //            new TestResult() { Outcome = UnitTestOutcome.Failed},
        //            new TestResult() { Outcome = UnitTestOutcome.Failed},
        //            new TestResult() { Outcome = UnitTestOutcome.Failed},
        //            new TestResult() { Outcome = UnitTestOutcome.Failed}
        //        };

        //    var args = It.IsAny<object[]>();
        //    //            var failingTestResult = new TestResult() { Outcome = UnitTestOutcome.Failed };

        //    int count = 0;
        //    mockPassingTestMethod.Setup(tm => tm.Invoke(args)).Returns(() =>
        //            {
        //                TestResult t = expected[count];
        //                count++;
        //                return t;
        //            }
        //    );

        //    const int RETRY_COUNT = 5;
        //    mockPassingTestMethod.Setup(tm => tm.GetAllAttributes(false)).Returns(() =>
        //        {
        //            Attribute[] attr = { new RetryAttribute(RETRY_COUNT) };
        //            return attr;
        //        }
        //    );

        //    var retriableTestMethod = new TestMethodExAttribute();

        //    // Act
        //    var tr = retriableTestMethod.Execute(mockPassingTestMethod.Object);

        //    // Assert
        //    mockPassingTestMethod.Verify(tm => tm.Invoke(args), Times.Exactly(RETRY_COUNT));
        //    CollectionAssert.AreEqual(expected, tr);
        //}

        //[TestMethodEx]
        //public void FlakyPassingTest_ExecutedtMultipleTimes_ReturnsPassed()
        //{
        //    // Arrange
        //    var mockFlakyTestMethod = new Mock<ITestMethod>();
        //    var args = It.IsAny<object[]>();
        //    TestResult[] expected =
        //        {
        //            new TestResult() { Outcome = UnitTestOutcome.Failed},
        //            new TestResult() { Outcome = UnitTestOutcome.Failed},
        //            new TestResult() { Outcome = UnitTestOutcome.Passed},
        //        };

        //    int count = 0;
        //    mockFlakyTestMethod.Setup(tm => tm.Invoke(args)).Returns(() =>
        //        {
        //            TestResult t = expected[count];
        //            count++;
        //            return t;
        //        }
        //    );

        //    const int RETRY_COUNT = 5;
        //    mockFlakyTestMethod.Setup(tm => tm.GetAllAttributes(false)).Returns(() =>
        //        {
        //            Attribute[] attr = { new RetryAttribute(RETRY_COUNT) };
        //            return attr;
        //        }
        //    );
        //    var retriableTestMethod = new TestMethodExAttribute();

        //    // Act
        //    var tr = retriableTestMethod.Execute(mockFlakyTestMethod.Object);

        //    // Assert
        //    mockFlakyTestMethod.Verify(tm => tm.Invoke(args), Times.Exactly(expected.Length));
        //    CollectionAssert.AreEqual(expected, tr);
        //}

        //[TestMethod]
        //public void FlakyFailingTest_ExecutedRetryCountNumberOfTimes_ReturnsFailed()
        //{
        //    // Arrange
        //    var mockFlakyTestMethod = new Mock<ITestMethod>();
        //    TestResult[] expected =
        //        {
        //            new TestResult() { Outcome = UnitTestOutcome.Failed},
        //            new TestResult() { Outcome = UnitTestOutcome.Failed},
        //            new TestResult() { Outcome = UnitTestOutcome.Failed},
        //            new TestResult() { Outcome = UnitTestOutcome.Failed},
        //            new TestResult() { Outcome = UnitTestOutcome.Failed},
        //            new TestResult() { Outcome = UnitTestOutcome.Failed},
        //            new TestResult() { Outcome = UnitTestOutcome.Failed},
        //            new TestResult() { Outcome = UnitTestOutcome.Passed},
        //        };
        //    var args = It.IsAny<object[]>();
        //    var passingTestResult = new TestResult() { Outcome = UnitTestOutcome.Passed };
        //    var failingTestResult = new TestResult() { Outcome = UnitTestOutcome.Failed };

        //    int count = 0;
        //    mockFlakyTestMethod.Setup(tm => tm.Invoke(args)).Returns(() =>
        //        {
        //            TestResult t = expected[count];
        //            count++;
        //            return t;
        //        }
        //    );

        //    const int RETRY_COUNT = 5;
        //    mockFlakyTestMethod.Setup(tm => tm.GetAllAttributes(false)).Returns(() =>
        //        {
        //            Attribute[] attr = { new RetryAttribute(RETRY_COUNT) };
        //            return attr;
        //        }
        //    );
        //    var retriableTestMethod = new TestMethodExAttribute();

        //    // Act
        //    var tr = retriableTestMethod.Execute(mockFlakyTestMethod.Object);

        //    // Assert
        //    mockFlakyTestMethod.Verify(tm => tm.Invoke(args), Times.Exactly(RETRY_COUNT));
        //    CollectionAssert.IsSubsetOf(tr, expected);
        //}
    }
}
