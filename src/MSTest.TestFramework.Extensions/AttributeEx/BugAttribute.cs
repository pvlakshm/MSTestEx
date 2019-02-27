using System;

namespace MSTest.TestFramework.Extensions.AttributeEx
{
    /// <summary>The BugAttribute add a bug ID to a test method.</summary>
    /// <param name="bugId">int representing the bug ID.</param>"
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class BugAttribute : Attribute
    {
        public BugAttribute(int bugId)
        {
            Value = bugId;
        }

        public int Value { get; }
    }
}
