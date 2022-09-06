using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp8Learning
{

    delegate IEnumerator<int> TestDel(int val, int repeatTimes);

    internal static class Test
    {
        public static IEnumerable<T> ChainExecuteMethods<T>(Delegate[] methods, params object[] paras)
        {
            bool finish = false;
            int count = 0;
            // while (count < methods.Length)
            List<IEnumerator<T>> result = new List<IEnumerator<T>>();
            foreach (var method in methods)
            {
                result.Add(((IEnumerator<T>)method.DynamicInvoke(paras)));
            }
            while (count < 3)
            {
                foreach (var item in result)
                {
                    if (item.MoveNext())
                    {
                        yield return item.Current;
                    }
                }
                count++;
            }
        }
    }
}
