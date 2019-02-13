using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTest.TestFramework.Extensions.AttributeEx
{
    /// <summary>Add an author to a test method or a test class.</summary>
    /// <param name="name">author name as a string</param>"
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class AuthorAttribute : TestPropertyAttribute
    {
        public AuthorAttribute(string name)
            : base("Author", name)
        {
        }
    }
}
