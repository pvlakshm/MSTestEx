using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTest.TestFramework.Extensions.AttributeEx
{
    /// <summary>Add a bug id to a test method.</summary>
    /// <param name="bugId">bug ID as a string</param>"
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class BugAttribute : TestPropertyAttribute
    {
        public BugAttribute(string bugId)
            : base("Bug", bugId)
        {
        }
    }
}
