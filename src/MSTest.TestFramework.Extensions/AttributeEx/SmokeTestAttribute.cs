using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTest.TestFramework.Extensions.AttributeEx
{
    /// <summary>The SmokeTestAttribute marks a test method as a smoke test.</summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class SmokeTestAttribute : Attribute
    {
        public SmokeTestAttribute() { }
    }
}
