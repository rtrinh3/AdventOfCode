using System.Diagnostics;

namespace Aoc2020
{
    // https://adventofcode.com/2020/day/25
    // --- Day 25: Combo Breaker ---
    public class Day25(string input) : IAocDay
    {
        private const int INITIAL_SUBJECT_NUMBER = 7;
        private const int MODULUS = 20201227;

        private static int GetLoopSize(int publicKey)
        {
            int value = 1;
            int loopSize = 0;
            while (value != publicKey)
            {
                // Transform
                value = (int)(Math.BigMul(value, INITIAL_SUBJECT_NUMBER) % MODULUS);
                loopSize++;
            }
            return loopSize;
        }

        private static int Transform(int value, int loop)
        {
            int ret = 1;
            for (int i = 0; i < loop; i++)
            {
                ret = (int)(Math.BigMul(ret, value) % MODULUS);
            }
            return ret;
        }

        public long Part1()
        {
            string[] keys = input.Split('\n');
            int cardPublicKey = int.Parse(keys[0]);
            int doorPublicKey = int.Parse(keys[1]);
            int cardLoopSize = GetLoopSize(cardPublicKey);
            int doorLoopSize= GetLoopSize(doorPublicKey);
            int cardDoorEncryptionKey = Transform(cardPublicKey, doorLoopSize);
            int doorCardEncryptionKey = Transform(doorPublicKey, cardLoopSize);
            Debug.Assert(cardDoorEncryptionKey == doorCardEncryptionKey);
            return cardDoorEncryptionKey;
        }

        public long Part2()
        {
            return MODULUS;
        }
    }
}
