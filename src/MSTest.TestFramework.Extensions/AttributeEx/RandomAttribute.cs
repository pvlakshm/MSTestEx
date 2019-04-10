using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.Globalization;

namespace MSTest.TestFramework.Extensions.AttributeEx
{
    public class RandomAttribute : Attribute, ITestDataSource
    {
        private RandomDataSource _src;

        public RandomAttribute(int min, int max, int count, bool distinct = false)
        {
            _src = new IntRandomDataSource(min, max, count, distinct);
        }

        // ITestDataSource has 2 methods: GetData and GetDisplayName.
        // GetData returns the data rows.
        // GetDisplayName returns the name of the test for a data row. This name is visible in the Test Explorer or
        // in the console. Note that in our case, we compose the display name as follows:
        //     the name of the DataTestMethod,
        //     followed by '('.
        //     followed by the data values as comma separated elements,
        //     followed by ')'.
        public IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            return _src.GetData(methodInfo);
        }

        public string GetDisplayName(MethodInfo methodInfo, object[] data)
        {
            if (data != null)
            {
                return string.Format(CultureInfo.CurrentCulture, "{0}({1})", methodInfo.Name, string.Join(",", data));
            }

            return null;
        }
    }

    abstract class RandomDataSource
    {
        
        public abstract IEnumerable<object[]> GetData(MethodInfo methodInfo);
    }

    class IntRandomDataSource : RandomDataSource
    {
        private int _min;
        private int _max;
        private int _count;

        public IntRandomDataSource(int min, int max, int count, bool distinct) {
            _min = min;
            _max = max;
            _count = count;
        }

        public override IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            Random r = new Random();
            int i1 = 0;
            int i2 = 0;

            for (int i = 0; i < _count; i++)
            {
                i1 = r.Next(_min, _max);
                i2 = r.Next(_min, _max);
                yield return new object[] {i1, i2};
            }
        }
    }

    class DoubleRandomDataSource : RandomDataSource
    {
        private double _min;
        private double _max;
        private int _count;

        public DoubleRandomDataSource(double min, double max, int count, bool distinct) {
            _min = min;
            _max = max;
            _count = count;
        }

        public override IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            Random r = new Random();
            double d1 = 0.0;
            double d2 = 0.0;

            for (int i = 0; i < _count; i++)
            {
                d1 = r.NextDouble() * (_max - _min) + _min;
                d2 = r.NextDouble() * (_max - _min) + _min;
                yield return new object[] {d1, d2};
            }
        }
    }
}
