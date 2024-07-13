﻿using System.Diagnostics;
using System.Numerics;

namespace Aoc2020
{
    // https://adventofcode.com/2020/day/13
    // --- Day 13: Shuttle Search ---
    public class Day13(string input) : IAocDay
    {
        public string Part1()
        {
            var lines = input.TrimEnd().ReplaceLineEndings("\n").Split('\n');
            long now = long.Parse(lines[0]);
            string[] busses = lines[1].Split(',');

            long earliestBus = -1;
            long earliestPassage = -1;
            foreach (string bus in busses)
            {
                if (long.TryParse(bus, out long validBus))
                {
                    long passage = (now / validBus) * validBus;
                    if (passage < now)
                    {
                        passage += validBus;
                    }
                    if (earliestPassage < 0 || passage < earliestPassage)
                    {
                        earliestBus = validBus;
                        earliestPassage = passage;
                    }
                }
            }
            long wait = earliestPassage - now;
            long answer = earliestBus * wait;
            return answer.ToString();
        }

        public string Part2()
        {
            var lines = input.TrimEnd().ReplaceLineEndings("\n").Split('\n');
            // (The first line in your input is no longer relevant.)
            string[] busses = lines[1].Split(',');
            List<(BigInteger Factor, BigInteger Remainder)> validBusses = [];
            for (int i = 0; i < busses.Length; i++)
            {
                if (BigInteger.TryParse(busses[i], out BigInteger validBus))
                {
                    // The bus needs to arrive at (t + i):
                    // t + i = 0 (mod bus)
                    // t = -i = bus - i (mod bus)
                    validBusses.Add((validBus, (i == 0) ? 0 : validBus - i));
                }
            }

            // https://en.wikipedia.org/wiki/Chinese_remainder_theorem#Existence_(constructive_proof)
            var (factorA, remainderA) = validBusses[0];
            for (int i = 1; i < validBusses.Count; i++)
            {
                var (factorB, remainderB) = validBusses[i];
                var bezoutCoefficients = ExtendedEuclideanAlgorithm(factorA, factorB);
                var newRemainderA = remainderA * factorB * bezoutCoefficients.BezoutB + remainderB * factorA * bezoutCoefficients.BezoutA;
                factorA = factorA * factorB;
                remainderA = ((newRemainderA % factorA) + factorA) % factorA;
            }
            Debug.Assert(remainderA < long.MaxValue);
            return remainderA.ToString();
        }

        // https://en.wikipedia.org/wiki/Extended_Euclidean_algorithm#Pseudocode
        private static (BigInteger BezoutA, BigInteger BezoutB) ExtendedEuclideanAlgorithm(BigInteger a, BigInteger b)
        {
            BigInteger old_r = a;
            BigInteger r = b;
            BigInteger old_s = 1;
            BigInteger s = 0;
            BigInteger old_t = 0;
            BigInteger t = 1;
            while (r != 0)
            {
                BigInteger quotient = old_r / r;
                (old_r, r) = (r, old_r - quotient * r);
                (old_s, s) = (s, old_s - quotient * s);
                (old_t, t) = (t, old_t - quotient * t);
            }
            return (old_s, old_t);
        }
    }
}
