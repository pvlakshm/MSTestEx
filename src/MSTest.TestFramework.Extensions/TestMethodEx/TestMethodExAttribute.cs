using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTest.TestFramework.Extensions.TestMethodEx
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TestMethodExAttribute : TestMethodAttribute
    {
        public override TestResult[] Execute(ITestMethod testMethod)
        {
            TestResult[] result = base.Execute(testMethod);
            return result;
        }
    }
}
