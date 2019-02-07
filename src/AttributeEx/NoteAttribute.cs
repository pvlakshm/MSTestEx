using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTest.TestFramework.AttributeEx
{
    // -------------------------------------------------------------------------------------------
    // A strongly typped "Note" attribute that allows attaching a string as a note to a testmethod
    // E.g.
    // [TestMethod]
    // [Note("CPU Intensive")]
    // public void myTestCase() { }
    // -------------------------------------------------------------------------------------------

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class NoteAttribute : TestPropertyAttribute
    {
        public NoteAttribute(string s)
            : base("Note", s)
        {
        }
    }
}
