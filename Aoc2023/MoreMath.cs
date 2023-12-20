using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023
{
    internal static class MoreMath
    {
        // https://en.wikipedia.org/wiki/Euclidean_algorithm#Implementations
        public static long Gcd(long a, long b)
        {
            while (b != 0)
            {
                var t = b;
                b = a % b;
                a = t;
            }
            return a;
        }

        // https://en.wikipedia.org/wiki/Least_common_multiple#Using_the_greatest_common_divisor
        public static long Lcm(long a, long b)
        {
            return (a * b) / Gcd(a, b);
        }
        public static BigInteger Lcm(BigInteger a, BigInteger b)
        {
            return (a * b) / BigInteger.GreatestCommonDivisor(a, b);
        }
    }
}
