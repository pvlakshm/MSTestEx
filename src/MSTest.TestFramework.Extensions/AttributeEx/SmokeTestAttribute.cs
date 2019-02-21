using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace STest.TestFramework.Extensions.AttributeEx
{
    /// <summary>Annotate a testmethod as a smoke test.</summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class SmokeTestAttribute : TestPropertyAttribute
    {
        public SmokeTestAttribute()
            : base("SmokeTest", null)
        {
        }
    }
}
