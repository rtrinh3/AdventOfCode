using AocCommon;
using System.Collections.Concurrent;
using System.Text;

namespace Aoc2016;

// https://adventofcode.com/2016/day/5
// --- Day 5: How About a Nice Game of Chess? ---
public class Day05(string input) : IAocDay
{
    private const int OUTPUT_LENGTH = 8;

    private record Output(int Iteration, char Digit);

    private readonly string seed = input.Trim();

    public string Part1()
    {
        ConcurrentBag<Output> outputs = new();
        int iterations = -1;
        int found = 0;
        Thread[] consumers = new Thread[Environment.ProcessorCount];
        for (int consumerIndex = 0; consumerIndex < consumers.Length; consumerIndex++)
        {
            Thread consumer = new Thread(() =>
            {
                var hasher = System.Security.Cryptography.MD5.Create();
                while (found < OUTPUT_LENGTH)
                {
                    int i = Interlocked.Increment(ref iterations);
                    var hashInput = seed + i;
                    var hash = hasher.ComputeHash(Encoding.ASCII.GetBytes(hashInput));
                    if (hash[0] == 0 && hash[1] == 0 && hash[2] <= 0x0F)
                    {
                        string digit = hash[2].ToString("x");
                        outputs.Add(new(i, digit[0]));
                        Interlocked.Increment(ref found);
                    }
                }
            });
            consumers[consumerIndex] = consumer;
            consumer.Start();
        }
        foreach (var consumer in consumers)
        {
            consumer.Join();
        }
        //Console.WriteLine(iterations + " iterations");
        var wantedOutputs = outputs.OrderBy(x => x.Iteration).Select(x => x.Digit).Take(OUTPUT_LENGTH);
        string answer = string.Join("", wantedOutputs);
        return answer;
    }

    public string Part2()
    {
        ConcurrentBag<Output>[] outputs = new ConcurrentBag<Output>[OUTPUT_LENGTH];
        for (int i = 0; i < outputs.Length; i++)
        {
            outputs[i] = new();
        }
        int iterations = -1;
        int found = 0;
        Thread[] consumers = new Thread[Environment.ProcessorCount];
        for (int consumerIndex = 0; consumerIndex < consumers.Length; consumerIndex++)
        {
            Thread consumer = new Thread(() =>
            {
                var hasher = System.Security.Cryptography.MD5.Create();
                while (found < OUTPUT_LENGTH)
                {
                    int i = Interlocked.Increment(ref iterations);
                    var hashInput = seed + i;
                    var hash = hasher.ComputeHash(Encoding.ASCII.GetBytes(hashInput));
                    if (hash[0] == 0 && hash[1] == 0 && hash[2] <= 0x0F)
                    {
                        int position = hash[2] & 0x0f;
                        if (position < OUTPUT_LENGTH)
                        {
                            int charValue = hash[3] >> 4;
                            string digit = charValue.ToString("x");
                            if (outputs[position].IsEmpty)
                            {
                                Interlocked.Increment(ref found);
                            }
                            outputs[position].Add(new(i, digit[0]));
                        }
                    }
                }
            });
            consumers[consumerIndex] = consumer;
            consumer.Start();
        }
        foreach (var consumer in consumers)
        {
            consumer.Join();
        }
        //Console.WriteLine(iterations + " iterations");
        var wantedOutputs = outputs.Select(bag => bag.MinBy(x => x.Iteration).Digit);
        string answer = string.Join("", wantedOutputs);
        return answer;
    }
}
