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

        // https://en.wikipedia.org/wiki/Extended_Euclidean_algorithm#Pseudocode
        public static (TNumber BezoutA, TNumber BezoutB) ExtendedEuclideanAlgorithm<TNumber>(TNumber a, TNumber b)
            where TNumber : INumber<TNumber>
        {
            TNumber old_r = a;
            TNumber r = b;
            TNumber old_s = TNumber.One;
            TNumber s = TNumber.Zero;
            TNumber old_t = TNumber.Zero;
            TNumber t = TNumber.One;
            while (!TNumber.IsZero(r))
            {
                var quotient = old_r / r;
                (old_r, r) = (r, old_r - quotient * r);
                (old_s, s) = (s, old_s - quotient * s);
                (old_t, t) = (t, old_t - quotient * t);
            }
            return (old_s, old_t);
        }
    }
}
