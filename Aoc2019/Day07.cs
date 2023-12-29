using System.Collections.Concurrent;
using System.Numerics;

namespace Aoc2019
{
    // https://adventofcode.com/2019/day/7
    public class Day07(string input) : IAocDay
    {
        public string Part1()
        {
            IReadOnlyList<int> phaseSettingValues = new int[] { 0, 1, 2, 3, 4 };
            BigInteger maxOutput = 0;
            //int[] maxPhaseSettings = Array.Empty<int>();
            foreach (var phaseSettings in IteratePermutations(phaseSettingValues))
            {
                var phaseSettingsList = phaseSettings.ToArray();
                IEnumerable<BigInteger> ampOutputs = new BigInteger[] { 0 };
                for (int i = 0; i < 5; i++)
                {
                    IntcodeInterpreter amp = new IntcodeInterpreter(input);
                    ampOutputs = amp.RunToEnd(new BigInteger[] { phaseSettingsList[i] }.Concat(ampOutputs));
                }
                // At this point, ampOutputs is the output from the last amp.
                BigInteger output = ampOutputs.Single();
                if (output > maxOutput)
                {
                    maxOutput = output;
                    //maxPhaseSettings = phaseSettingsList;
                }
            }
            //Console.WriteLine(maxOutput);
            //Console.WriteLine(string.Join(',', maxPhaseSettings));
            return maxOutput.ToString();
        }

        public string Part2()
        {
            IReadOnlyList<int> phaseSettingValues = new int[] { 5, 6, 7, 8, 9 };
            BigInteger maxOutput = 0;
            //int[] maxPhaseSettings = Array.Empty<int>();
            foreach (var phaseSettings in IteratePermutations(phaseSettingValues))
            {
                var phaseSettingsList = phaseSettings.ToArray();
                BlockingCollection<BigInteger> feedbackLoop = new() { 0 };
                IEnumerable<BigInteger> ampOutputs = feedbackLoop.GetConsumingEnumerable();
                for (int i = 0; i < 5; i++)
                {
                    IntcodeInterpreter amp = new IntcodeInterpreter(input);
                    ampOutputs = amp.RunToEnd(new BigInteger[] { phaseSettingsList[i] }.Concat(ampOutputs));
                }
                // At this point, ampOutputs is the output from the last amp.
                // We need to record the signals and send them back to the first amp.
                BigInteger lastOutput = 0;
                foreach (var signal in ampOutputs)
                {
                    lastOutput = signal;
                    feedbackLoop.Add(signal);
                }
                if (lastOutput > maxOutput)
                {
                    maxOutput = lastOutput;
                    //maxPhaseSettings = phaseSettingsList;
                }
            }
            //Console.WriteLine(maxOutput);
            //Console.WriteLine(string.Join(',', maxPhaseSettings));
            return maxOutput.ToString();
        }

        private static IEnumerable<IEnumerable<T>> IteratePermutations<T>(IEnumerable<T> alphabet)
        {
            if (!alphabet.Any())
            {
                yield return Array.Empty<T>();
            }
            else
            {
                foreach (T letter in alphabet)
                {
                    T[] stub = { letter };
                    var otherPermutations = IteratePermutations(alphabet.Where(x => !object.Equals(x, letter)));
                    foreach (var other in otherPermutations)
                    {
                        yield return stub.Concat(other);
                    }
                }
            }
        }
    }
}
