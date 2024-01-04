using System.Collections.Concurrent;
using System.Numerics;

namespace Aoc2019
{
    // https://adventofcode.com/2019/day/23
    public class Day23 : IAocDay
    {
        private const int NUMBER_COMPUTERS = 50;

        private readonly Lazy<BigInteger> part1Answer;
        private readonly Lazy<BigInteger> part2Answer;

        public Day23(string input)
        {
            Dictionary<BigInteger, BlockingCollection<(BigInteger, BigInteger)>> bus = new();
            for (int i = 0; i < NUMBER_COMPUTERS; i++)
            {
                bus[i] = new BlockingCollection<(BigInteger, BigInteger)>();
            }
            bus[255] = new BlockingCollection<(BigInteger, BigInteger)>();

            Thread[] computerTasks = new Thread[NUMBER_COMPUTERS];
            int waitingComputers = 0;
            for (int i = 0; i < NUMBER_COMPUTERS; i++)
            {
                int address = i;
                var task = new Thread(new ThreadStart(() =>
                {
                    IntcodeInterpreter interpreter = new(input);
                    IEnumerable<BigInteger> queueInputs()
                    {
                        yield return address;
                        yield return -1; // Kickstart
                        while (true)
                        {
                            Interlocked.Increment(ref waitingComputers);
                            var res = bus[address].Take();
                            Interlocked.Decrement(ref waitingComputers);
                            yield return res.Item1;
                            yield return res.Item2;
                        }
                    }
                    var outputs = interpreter.RunToEnd(queueInputs());
                    try
                    {
                        foreach (var output in outputs.Chunk(3))
                        {
                            //Console.WriteLine($"Send from {address} to {output[0]} ({output[1]}, {output[2]})");
                            bus[output[0]].Add((output[1], output[2]));
                        }
                    }
                    catch (ThreadInterruptedException)
                    {
                        return;
                    }
                }));
                task.Start();
                computerTasks[address] = task;
            }

            var part1Queue = new BlockingCollection<BigInteger>();
            part1Answer = new Lazy<BigInteger>(() => part1Queue.Take());
            var part2Queue = new BlockingCollection<BigInteger>();
            part2Answer = new Lazy<BigInteger>(() => part2Queue.Take());

            Thread natTask = new Thread(new ThreadStart(() =>
            {
                (BigInteger, BigInteger)? lastWake = null;
                bool partOneDone = false;
                while (true)
                {
                    while (true)
                    {
                        Thread.Sleep(50);
                        if (waitingComputers == NUMBER_COMPUTERS)
                        {
                            break;
                        }
                    }
                    (BigInteger, BigInteger)? valHolder = null;
                    while (bus[255].Any())
                    {
                        valHolder = bus[255].Take();
                        if (!partOneDone)
                        {
                            part1Queue.Add(valHolder.Value.Item2);
                            partOneDone = true;
                        }
                    }
                    if (valHolder == null)
                    {
                        throw new Exception("NAT received nothing");
                    }
                    var val = valHolder.Value;
                    bus[0].Add(val);
                    //Console.WriteLine($"Wake up 0 with {val}");
                    if (lastWake.HasValue && val.Item2 == lastWake.Value.Item2)
                    {
                        part2Queue.Add(val.Item2);
                        break;
                    }
                    lastWake = val;
                }
                // End
                foreach (var t in computerTasks)
                {
                    t.Interrupt();
                }
            }));
            natTask.Start();
        }

        public string Part1()
        {
            return part1Answer.Value.ToString();
        }

        public string Part2()
        {
            return part2Answer.Value.ToString();
        }
    }
}
