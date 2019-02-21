using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTest.TestFramework.Extensions.AttributeEx
{
    /// <summary>The BugAttribute add a bug ID to a test method.</summary>
    /// <param name="bugId">int representing the bug ID.</param>"
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class BugAttribute : TestPropertyAttribute
    {
        public BugAttribute(int bugId)
            : base("Bug", bugId.ToString())
        {
        }
    }
}
