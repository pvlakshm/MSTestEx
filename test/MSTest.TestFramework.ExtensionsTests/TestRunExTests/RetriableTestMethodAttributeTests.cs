using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.TestFramework.Extensions;

namespace MSTest.TestFramework.ExtensionsTests.TestRunExTests
{
    [TestClass]
    public class RetriableTestMethodAttributeTests
    {
        [TestMethod]
        public void ExecuteShouldRunTheTestOnceIfItPasses()
        {
        }

        [TestMethod]
        public void ExecuteShouldRunTheTestMultipleTimesTillItPasses()
        {
        }

        [TestMethod]
        public void ExecuteShouldRunTheTestRetryCountNumberOfTimesAndReturnFailure()
        {
        }
    }
}
