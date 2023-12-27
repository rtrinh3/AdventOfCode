using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AocCommon
{
    public static class Memoization
    {
        public static Func<T1, TR> Make<T1, TR>(Func<T1, TR> fn)
            where T1 : notnull
        {
            Dictionary<T1, TR> memo = new();
            return x1 =>
            {
                if (memo.TryGetValue(x1, out var result))
                {
                    return result;
                }
                return memo[x1] = fn(x1);
            };
        }
        public static Func<T1, T2, TR> Make<T1, T2, TR>(Func<T1, T2, TR> fn)
        {
            Dictionary<(T1, T2), TR> memo = new();
            return (x1, x2) =>
            {
                if (memo.TryGetValue((x1, x2), out var result))
                {
                    return result;
                }
                return memo[(x1, x2)] = fn(x1, x2);
            };
        }
    }
}
