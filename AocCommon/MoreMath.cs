using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AocCommon
{
    public static class MoreMath
    {
        // https://en.wikipedia.org/wiki/Euclidean_algorithm#Implementations
        public static TInt Gcd<TInt>(TInt a, TInt b)
            where TInt : IBinaryInteger<TInt>
        {
            while (!TInt.IsZero(b))
            {
                var t = b;
                b = a % b;
                a = t;
            }
            return a;
        }

        // https://en.wikipedia.org/wiki/Least_common_multiple#Using_the_greatest_common_divisor
        public static TInt Lcm<TInt>(TInt a, TInt b)
            where TInt : IBinaryInteger<TInt>
        {
            return (a * b) / Gcd(a, b);
        }
        public static BigInteger Lcm(BigInteger a, BigInteger b)
        {
            return (a * b) / BigInteger.GreatestCommonDivisor(a, b);
        }
    }
}
