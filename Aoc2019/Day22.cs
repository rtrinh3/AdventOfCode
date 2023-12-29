using System.Numerics;
using System.Text.RegularExpressions;

namespace Aoc2019
{
    // https://adventofcode.com/2019/day/22
    public class Day22(string input) : IAocDay
    {
        readonly string[] lines = input.Split('\n', AocCommon.Constants.TrimAndDiscard);

        public string Part1()
        {
            return DoPart1(10007, 2019).ToString();
        }

        public int DoPart1(int deckSize, int cardToTrack)
        {
            int TrackCard(int cardToTrack)
            {
                foreach (var cmd in lines)
                {
                    if (cmd == "deal into new stack")
                    {
                        cardToTrack = deckSize - cardToTrack - 1;
                        continue;
                    }
                    var matchCut = Regex.Match(cmd, @"^cut (-?\d+)");
                    if (matchCut.Success)
                    {
                        int cut = -int.Parse(matchCut.Groups[1].Value);
                        if (cut < 0) cut += deckSize;
                        cardToTrack = (cardToTrack + cut) % deckSize;
                        continue;
                    }
                    var matchSwizzle = Regex.Match(cmd, @"^deal with increment (\d+)");
                    if (matchSwizzle.Success)
                    {
                        int swizzle = int.Parse(matchSwizzle.Groups[1].Value);
                        cardToTrack = (cardToTrack * swizzle) % deckSize;
                        continue;
                    }
                    throw new Exception("Unknown technique");
                }
                return cardToTrack;
            }
            int newPos = TrackCard(cardToTrack);
            return newPos;
        }

        public string Part2()
        {
            return DoPart2(119315717514047, 101741582076661, 2020).ToString();
        }

        public BigInteger DoPart2(BigInteger deckSize, BigInteger repeatShuffle, BigInteger cardPos)
        {
            // BigInteger avoids the overflows we get with long
            var (coefficient, addend) = CalculateReverseMovement();
            var bigCoefficient = ExpMod(coefficient, repeatShuffle, deckSize);
            var bigAddend = (addend * GeometricSumMod(coefficient, repeatShuffle, deckSize)) % deckSize;
            var newPos = (bigCoefficient * cardPos + bigAddend) % deckSize;
            if (newPos < 0) newPos += deckSize;
            return newPos;

            (BigInteger, BigInteger) CalculateReverseMovement()
            {
                // The movement of the cards can be represented as (coefficient * cardPos + addend) mod deckSize
                BigInteger coefficient = 1;
                BigInteger addend = 0;
                foreach (var cmd in lines.AsEnumerable().Reverse())
                {
                    if (cmd == "deal into new stack")
                    {
                        coefficient = -coefficient;
                        addend = (-addend - 1) % deckSize;
                        continue;
                    }
                    var matchCut = Regex.Match(cmd, @"^cut (-?\d+)");
                    if (matchCut.Success)
                    {
                        BigInteger cut = BigInteger.Parse(matchCut.Groups[1].Value);
                        if (cut < 0) cut += deckSize;
                        addend = (addend + cut) % deckSize;
                        continue;
                    }
                    var matchSwizzle = Regex.Match(cmd, @"^deal with increment (\d+)");
                    if (matchSwizzle.Success)
                    {
                        BigInteger swizzle = BigInteger.Parse(matchSwizzle.Groups[1].Value);
                        BigInteger inverseSwizzle = ModInverse(swizzle, deckSize);
                        coefficient = (coefficient * inverseSwizzle) % deckSize;
                        addend = (addend * inverseSwizzle) % deckSize;
                        continue;
                    }
                    throw new Exception("Unknown technique");
                }
                return (coefficient, addend);
            }
        }

        
        private static BigInteger ModInverse(BigInteger x, BigInteger mod)
        {
            // https://en.wikipedia.org/wiki/Extended_Euclidean_algorithm#Modular_integers
            BigInteger t = 0;
            BigInteger newt = 1;
            BigInteger r = mod;
            BigInteger newr = x;
            while (newr != 0)
            {
                var quotient = r / newr;
                (t, newt) = (newt, t - quotient * newt);
                (r, newr) = (newr, r - quotient * newr);
            }
            if (r > 1)
            {
                throw new Exception("not invertible");
            }
            if (t < 0)
            {
                t += mod;
            }
            return t;
        }
        private static BigInteger ExpMod(BigInteger b, BigInteger n, BigInteger mod)
        {
            // https://en.wikipedia.org/wiki/Exponentiation_by_squaring#With_constant_auxiliary_memory
            // Added Mod
            if (n < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(n));
            }
            else if (n == 0)
            {
                return 1;
            }
            BigInteger y = 1;
            while (n > 1)
            {
                if (n % 2 == 1)
                {
                    y = (b * y) % mod;
                    n--;
                }
                b = (b * b) % mod;
                n /= 2;
            }
            return (b * y) % mod;
        }
        /// Calculates 1 + b + b^2 + b^3 + b^4 + ... + b^(n - 1), modulo m
        private static BigInteger GeometricSumMod(BigInteger b, BigInteger n, BigInteger mod)
        {
            // https://stackoverflow.com/a/42033401
            BigInteger t = 1;
            BigInteger e = b % mod;
            BigInteger total = 0;
            while (n > 0)
            {
                if (n % 2 == 1)
                {
                    total = (e * total + t) % mod;
                }
                t = ((e + 1) * t) % mod;
                e = (e * e) % mod;
                n = n / 2;
            }
            return total;
        }
    }
}
