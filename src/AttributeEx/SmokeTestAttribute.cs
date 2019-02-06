using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTest.TestFramework.AttributeEx
{
    // -------------------------------------------------------------------------------------------
    // A strongly typed attribute that allows categorizing a test method as a so called SmokeTest.
    // Usage
    // [TestMethod]
    // [SmokeTest]
    // public void myTestCase() { }
    // -------------------------------------------------------------------------------------------

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class SmokeTestAttribute : TestPropertyAttribute
    {
        public SmokeTestAttribute()
            : base("SmokeTest", null)
        {
        }
    }
}