using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.Globalization;

namespace MSTest.TestFramework.Extensions.AttributeEx
{
    /// <summary>The RandomAttribute is used to specify a set of random values to be provided for an individual numeric parameter of a data driven test method. Random value within the given range will be generated and cast to the parameter types.</summary>
    /// <param name="min">int representing the inclusive lower bound of the random number generated.</param>"
    /// <param name="max">int representing the exclusive lower bound of the random number generated.</param>"
    public class RandomAttribute : Attribute, ITestDataSource
    {
        private int _min;
        private int _max;

        public RandomAttribute(
            int min,
            int max)
        {
            if (max <= min)
            {
                throw new Exception("RandomAttribute bounds specification error");
            }

            _min = min;
            _max = max;
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
            /*
            first, find out how many params need to be returned, and the type of each param.
            Next for each param that needs to be returned,
                generate a random value and store it in a variable of the associated Type

            Finally, make an object array of all such vars and return it.
            */
            ParameterInfo[] paramImnfo = methodInfo.GetParameters();
            int numOfParams = paramImnfo.Length;

            if (numOfParams == 0)
            {
                yield return null;
            }

            Type[] paramTypes = new Type[numOfParams];
            for (int i = 0; i < numOfParams; i++)
            {
                paramTypes[i] = paramImnfo[i].ParameterType;
            }

            object[] pars = new object[numOfParams];
            RandomDataSource _src = new RandomDataSource();

            for (int i = 0; i < numOfParams; i++)
            {
                if (paramTypes[i] == typeof(int) ||
                    paramTypes[i] == typeof(uint) ||
                    paramTypes[i] == typeof(short) ||
                    paramTypes[i] == typeof(ushort) ||
                    paramTypes[i] == typeof(byte) ||
                    paramTypes[i] == typeof(long) ||
                    paramTypes[i] == typeof(ulong))
                {
                    int val = _src.GetNextInt(_min, _max);
                    pars[i] = Convert.ChangeType(val, paramTypes[i]);
                }
                else if (paramTypes[i] == typeof(float) ||
                        paramTypes[i] == typeof(double))
                {
                    double val = _src.GetNextDouble(_min, _max);
                    pars[i] = Convert.ChangeType(val, paramTypes[i]);
                }

            }

            yield return pars;
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

    class RandomDataSource
    {
        public int GetNextInt(int min, int max)
        {
            Random r = new Random();
            int i1 = r.Next(min, max);
            return i1;
        }

        public double GetNextDouble(int min, int max)
        {
            double dmin = (double) min;
            double dmax = (double) max;

            Random r = new Random();
            double d1 = r.NextDouble() * (dmax - dmin) + dmin;

            return d1;
        }
    }
}
