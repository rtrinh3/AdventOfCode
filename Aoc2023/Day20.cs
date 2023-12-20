using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023
{
    // https://adventofcode.com/2023/day/20
    public class Day20 : IAocDay
    {
        private Dictionary<string, IModule> modules = new();
        public Day20(string input)
        {
            Dictionary<string, List<string>> targetToSourceMap = new();
            void AddTargetToSourceConnection(string dst, string src)
            {
                if (!targetToSourceMap.TryGetValue(dst, out var value))
                {
                    value = new List<string>();
                    targetToSourceMap[dst] = value;
                }
                value.Add(src);
            }
            List<(string, NandModule)> nandModules = new();
            string[] lines = input.ReplaceLineEndings("\n").Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            foreach (string line in lines)
            {
                var parts = line.Split("->", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                string source = parts[0];
                string[] targets = parts[1].Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                string actualName;
                switch (source[0])
                {
                    case '%':
                        actualName = source[1..^0];
                        modules[actualName] = new FlipFlopModule(actualName, targets);
                        break;
                    case '&':
                        actualName = source[1..^0];
                        NandModule nand = new NandModule(actualName, targets);
                        modules[actualName] = nand;
                        nandModules.Add((actualName, nand));
                        break;
                    default:
                        actualName = source;
                        modules[actualName] = new BroadcastModule(actualName, targets);
                        break;
                }
                foreach (string target in targets)
                {
                    AddTargetToSourceConnection(target, actualName);
                }
            }
            foreach (string target in targetToSourceMap.Keys)
            {
                modules.TryAdd(target, new BroadcastModule(target, []));
            }
            foreach (var (name, nand) in nandModules)
            {
                foreach (var src in targetToSourceMap[name])
                {
                    nand.AddSource(src);
                }
            }
        }

        public long Part1()
        {
            ResetModules();
            Queue<Impulse> pulseQueue = new();
            long lows = 0;
            long highs = 0;
            for (int i = 0; i < 1000; i++)
            {
                pulseQueue.Enqueue(new Impulse("button", "broadcaster", false));
                while (pulseQueue.TryDequeue(out var pulse))
                {
                    //Console.WriteLine(pulse);
                    if (pulse.Pulse)
                    {
                        highs++;
                    }
                    else
                    {
                        lows++;
                    }
                    var outputs = modules[pulse.Target].SendPulse(pulse.Source, pulse.Pulse);
                    foreach (var output in outputs)
                    {
                        pulseQueue.Enqueue(output);
                    }
                }
            }

            return lows * highs;
        }
        public long Part2()
        {
            ResetModules();
            Queue<Impulse> pulseQueue = new();
            long i = 0;
            bool done = false;
            Task.Run(() =>
            {
                while (!done)
                {
                    Console.WriteLine($"Button {i}");
                    Thread.Sleep(1000);
                }
            });
            while (true)
            {
                i++;
                pulseQueue.Enqueue(new Impulse("button", "broadcaster", false));
                while (pulseQueue.TryDequeue(out var pulse))
                {
                    //Console.WriteLine(pulse);
                    if (pulse.Target == "rx")
                    {
                        if (!pulse.Pulse)
                        {
                            done = true;
                            return i;
                        }
                        //else
                        //{
                        //    Console.WriteLine("rx got high pulse");
                        //}
                    }
                    var outputs = modules[pulse.Target].SendPulse(pulse.Source, pulse.Pulse);
                    foreach (var output in outputs)
                    {
                        pulseQueue.Enqueue(output);
                    }
                }
            }
            // TODO Cycle detection of the sources of the source of "rx"
        }

        private void ResetModules()
        {
            foreach (var mod in modules.Values)
            {
                mod.Reset();
            }
        }

        // true is high pulse, false is low pulse
        private record Impulse(string Source, string Target, bool Pulse);

        private interface IModule
        {
            IEnumerable<Impulse> SendPulse(string source, bool pulse);
            void Reset();
        }

        private class BroadcastModule(string name, IEnumerable<string> targets) : IModule
        {
            public IEnumerable<Impulse> SendPulse(string source, bool pulse)
            {
                return targets.Select(t => new Impulse(name, t, pulse));
            }
            public void Reset() { }
        }
        private class FlipFlopModule(string name, IEnumerable<string> targets) : IModule
        {
            private bool memory = false;
            public IEnumerable<Impulse> SendPulse(string source, bool pulse)
            {
                if (!pulse)
                {
                    memory = !memory;
                    return targets.Select(t => new Impulse(name, t, memory));
                }
                else
                {
                    return [];
                }
            }
            public void Reset()
            {
                memory = false;
            }
        }
        private class NandModule(string name, IEnumerable<string> targets) : IModule
        {
            private Dictionary<string, bool> sources = new();
            public void AddSource(string source)
            {
                sources[source] = false;
            }
            public IEnumerable<Impulse> SendPulse(string source, bool pulse)
            {
                sources[source] = pulse;
                bool output = !sources.Values.All(x => x);
                return targets.Select(t => new Impulse(name, t, output));
            }
            public void Reset()
            {
                foreach (string source in sources.Keys)
                {
                    sources[source] = false;
                }
            }
        }
    }
}
