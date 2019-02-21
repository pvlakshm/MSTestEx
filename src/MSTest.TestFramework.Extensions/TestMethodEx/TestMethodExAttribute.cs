using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.TestFramework.Extensions.AttributeEx;
using System.Reflection;
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

            TestResult[] result = null;
            Attribute[] attr = testMethod.GetAllAttributes(false);

            if (attr == null)
            {
                result = base.Execute(testMethod);
                return result;
            }

            foreach (Attribute a in attr)
            {
                if (a is RetryAttribute)
                {
                    RetryAttribute retryAttr = (RetryAttribute)a;
                    int retryCount = int.Parse(retryAttr.Value);

                    var currentCount = 0;

                    while (currentCount < retryCount)
                    {
                        try
                        {
                            result = base.Execute(testMethod);
                        }
                        catch (Exception) { }

                        currentCount++;

                        if (result.Any((tr) => tr.Outcome == UnitTestOutcome.Failed))
                        {
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }

                    return result;
                }
            }

            result = base.Execute(testMethod);
            return result;
        }
    }
}
