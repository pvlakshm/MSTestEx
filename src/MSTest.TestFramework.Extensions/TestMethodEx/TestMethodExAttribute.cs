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
        public override TestResult[] Execute(ITestMethod testMethod)
        {
            // NOTE
            // This implementation will need to be refactored as we add more
            // execution variations.

            int retryCount = 1;
            Type eet;
            System.Attribute[] attr = testMethod.GetAllAttributes(false);

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
                    if (a is RetryAttribute)
                    {
                        RetryAttribute retryAttr = (RetryAttribute) a;
                        retryCount = retryAttr.Value;
                    }
                }
            }

            TestResult[] results = null;
            var res = new List<TestResult>();

            //////
            for (int count = 0; count < retryCount; count++)
            {
                var testResults = base.Execute(testMethod);

                if (testResults.Any((tr) => tr.Outcome == UnitTestOutcome.Failed))
                {
                    foreach (var testResult in testResults)
                    {
                        testResult.DisplayName = $"{testMethod.TestMethodName} - Execution attempt {count + 1}";
                        res.AddRange(testResults);
                    }
                }
                else
                {
                    res.AddRange(testResults);
                    break;
                }
            }

            return res.ToArray();
            //////


            var currentCount = 0;
            while (currentCount < retryCount)
            {
                currentCount++;

                try
                {
                    results = base.Execute(testMethod);
                }
                catch (Exception e)
                {
                    if (eet == null)
                    {
                        break;
                    }

                    if (e.GetType().Equals(eet) == false)
                    {
                        break;
                    }
                }

                if (results == null)
                {
                    continue;
                }

                foreach (var testResult in results)
                {
                    testResult.DisplayName = $"{testMethod.TestMethodName} - Execution number {currentCount}";
                }
                res.AddRange(results);

                if (results.Any((tr) => tr.Outcome == UnitTestOutcome.Failed))
                {
                    continue;
                }

                break;
            }

            return res.ToArray();
        }
    }
}
