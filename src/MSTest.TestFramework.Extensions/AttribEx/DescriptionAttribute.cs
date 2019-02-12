using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTest.TestFramework.Extensions.AttributeEx
{
    /// <summary>Add descriptive text to a test. Supported a TestMethod and a TestClass.</summary>
    /// <param name="desc">description as a string</param>"
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class DescriptionAttribute : TestPropertyAttribute
    {
        public DescriptionAttribute(string s)
            : base("Description", s)
        {
        }
    }
}
