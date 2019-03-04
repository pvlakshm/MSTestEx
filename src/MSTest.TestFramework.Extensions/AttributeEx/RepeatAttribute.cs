using System;
using System.Collections.Generic;
using System.Text;

namespace MSTest.TestFramework.Extensions.AttributeEx
{
    /// <summary>The RepeatAttribute is used on a test method to specify that it should be executed multiple times. If any repetition fails, the remaining ones are not run and a failure is reported.</summary>
    /// <param name="repeatCount">int representing the repitition count.</param>"
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class RepeatAttribute : Attribute
    {
        private const int MIN_REPEAT_COUNT = 1;
        private const int MAX_REPEAT_COUNT = 50;

        public int Value { get; }

        public RepeatAttribute(int repeatCount)
        {
            if (repeatCount < MIN_REPEAT_COUNT || MAX_REPEAT_COUNT < repeatCount)
            {
                repeatCount = MIN_REPEAT_COUNT;
            }

            Value = repeatCount;
        }
    }
}
