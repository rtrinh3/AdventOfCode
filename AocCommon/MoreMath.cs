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
        public static BigInteger Gcd(BigInteger a, BigInteger b)
        {
            return BigInteger.GreatestCommonDivisor(a, b);
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

        public static IEnumerable<T[]> IteratePermutations<T>(IEnumerable<T> alphabet)
        {
            return IteratePermutationsImpl(alphabet).Select(p => p.ToArray());
        }

        private static IEnumerable<IEnumerable<T>> IteratePermutationsImpl<T>(IEnumerable<T> alphabet)
        {
            if (!alphabet.Any())
            {
                yield return Array.Empty<T>();
            }
            foreach (var head in alphabet)
            {
                var restOfAlphabet = alphabet.Where(x => !object.Equals(head, x));
                var tails = IteratePermutationsImpl(restOfAlphabet);
                foreach (var tail in tails)
                {
                    yield return tail.Prepend(head);
                }
            }
        }
    }
}
