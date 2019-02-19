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
            TestResult[] result = null;
            MethodInfo m = testMethod.MethodInfo;
            var attr = m.GetCustomAttribute(typeof(RetryAttribute), false);

            if (attr != null)
            {
                RetryAttribute retryAttr = (RetryAttribute)attr;
                int retryCount = int.Parse(retryAttr.Value);

                var currentCount = 0;

                while (currentCount <= retryCount)
                {
                    try
                    {
                        result = base.Execute(testMethod);
                    }
                    catch (Exception) { }

                    currentCount++;

                    if (result.Any((tr) => tr.Outcome == UnitTestOutcome.Failed))
                    {
                        if (currentCount < retryCount)
                            continue;
                    }

                    break;
                }

                return result;
            }
            else
            {
                result = base.Execute(testMethod);
                return result;
            }
        }
    }
}
