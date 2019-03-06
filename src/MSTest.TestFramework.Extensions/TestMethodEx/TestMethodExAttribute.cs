using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.TestFramework.Extensions.AttributeEx;
using System.Collections.Generic;
using System.Linq;

namespace MSTest.TestFramework.Extensions.TestMethodEx
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TestMethodExAttribute : TestMethodAttribute
    {
        public TestResult[] executeWithRetryOnFailure(
            ITestMethod testMethod,
            int retryCount)
        {
            var res = new List<TestResult>();

            for (int count = 0; count < retryCount; count++)
            {
                var testResults = base.Execute(testMethod);
                res.AddRange(testResults);

                if (testResults.Any((tr) => tr.Outcome == UnitTestOutcome.Failed))
                {
                    continue;
                }

                break;
            }

            return res.ToArray();
        }

        public override TestResult[] Execute(
            ITestMethod testMethod)
        {
            // NOTE
            // This implementation will need to be refactored as we add more
            // execution variations.

            int retryCount = 1;
            int repeatCount = 1;

            Attribute[] attr = testMethod.GetAllAttributes(false);
            if (attr == null)
            {
                Attribute[] r1 = testMethod.GetAttributes<RetryAttribute>(false);
                var attr2 = new List<Attribute>();
                attr2.AddRange(r1);
                attr = attr2.ToArray();
            }

            if (attr != null)
            {
                foreach (Attribute a in attr)
                {
                    if (a is RetryAttribute retryAttr)
                    {
                        retryCount = retryAttr.Value;
                    }
                    if (a is RepeatAttribute repeatAttr)
                    {
                        repeatCount = repeatAttr.Value;
                        // need to add support.
                    }
                }
            }

            var res = executeWithRetryOnFailure(testMethod, retryCount);
            return res;
        }
    }
}
