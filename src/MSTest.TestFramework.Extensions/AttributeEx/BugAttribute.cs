using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTest.TestFramework.Extensions.AttributeEx
{
    /// <summary>The BugAttribute add a bug ID to a test method.</summary>
    /// <param name="bugId">int representing the bug ID.</param>"
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class BugAttribute : Attribute
    {
        private readonly int ID;

        public BugAttribute(int bugId)
        {
            ID = bugId;
        }

        public int Value
        {
            get
            {
                return ID;
            }
        }
    }
}
