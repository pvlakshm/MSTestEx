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
        //public override TestResult[] Execute(ITestMethod testMethod)
        //{
        //    // NOTE
        //    // This implementation will need to be refactored as we add more
        //    // execution variations.

        //    TestResult[] result = null;
        //    Attribute[] attr = testMethod.GetAllAttributes(false);

        //    if (attr == null)
        //    {
        //        result = base.Execute(testMethod);
        //        return result;
        //    }

        //    foreach (Attribute a in attr)
        //    {
        //        if (a is RetryAttribute)
        //        {
        //            RetryAttribute retryAttr = (RetryAttribute)a;
        //            int retryCount = int.Parse(retryAttr.Value);

        //            var currentCount = 0;

        //            while (currentCount < retryCount)
        //            {
        //                try
        //                {
        //                    result = base.Execute(testMethod);
        //                }
        //                catch (Exception) { }

        //                currentCount++;

        //                if ((result == null) ||
        //                       (result.Any((tr) => tr.Outcome == UnitTestOutcome.Failed)))
        //                {
        //                    continue;
        //                }
        //                else
        //                {
        //                    break;
        //                }
        //            }

        //            return result;
        //        }
        //    }

        //    result = base.Execute(testMethod);
        //    return result;
        //}

        public override TestResult[] Execute(ITestMethod testMethod)
        {
            // NOTE
            // This implementation will need to be refactored as we add more
            // execution variations.

            int retryCount = 1;
            Type eet = null;
            Attribute[] attr = testMethod.GetAllAttributes(false);

            if (attr != null)
            {
                foreach (Attribute a in attr)
                {
                    if (a is RetryAttribute)
                    {
                        RetryAttribute retryAttr = (RetryAttribute)a;
                        retryCount = int.Parse(retryAttr.Value);
                    }

                    if (a is ExpectedExceptionAttribute)
                    {
                        ExpectedExceptionAttribute eea = (ExpectedExceptionAttribute)a;
                        eet = eea.ExceptionType;
                    }
                }
            }

            TestResult[] results = null;
            var res = new List<TestResult>();

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
