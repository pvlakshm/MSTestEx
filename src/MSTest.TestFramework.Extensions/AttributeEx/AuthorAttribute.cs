using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTest.TestFramework.Extensions.AttributeEx
{
    /// <summary>The AuthorAttribute adds information about the author of a test method.</summary>
    /// <param name="name">string representing the author name.</param>"
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class AuthorAttribute : Attribute
    {
        private readonly string author;

        public AuthorAttribute(string name)
        {
            author = name;
        }

        public string Value
        {
            get
            {
                return author;
            }

        }
    }
}
