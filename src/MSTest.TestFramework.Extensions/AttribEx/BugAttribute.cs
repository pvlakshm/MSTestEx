using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTest.TestFramework.Extensions.AttributeEx
{
    /// <summary>Add a bug id to a test method.</summary>
    /// <param name="bugId">bug ID as an integer</param>"
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class BugAttribute : TestPropertyAttribute
    {
        public BugAttribute(int bugId)
            : base("Bug", bugId.ToString())
        {
        }
    }
}
