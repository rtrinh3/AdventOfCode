using System.Numerics;
using System.Text.RegularExpressions;
using System.Text;

namespace Aoc2019
{
    // https://adventofcode.com/2019/day/25
    public class Day25(string input) : IAocDay
    {
        public string Part1()
        {
            IntcodeInterpreter interpreter = new(input);
            StringBuilder log = new();
            static IEnumerable<BigInteger> manualInputs()
            {
                string? line;
                while ((line = Console.ReadLine()) != null)
                {
                    foreach (char c in line)
                    {
                        yield return (BigInteger)c;
                    }
                    yield return (BigInteger)'\n';
                }
            }
            IEnumerable<BigInteger> automaticInputs()
            {
                string takeAll = @"east
east
east
take shell
west
south
take monolith
north
west
north
west
take bowl of rice
east
north
take planetoid
west
take ornament
south
south
take fuel cell
north
north
east
east
take cake
south
west
north
take astrolabe
west
";
                foreach (char c in takeAll.ReplaceLineEndings("\n"))
                {
                    yield return (BigInteger)c;
                }
                string[] items = {
                    "monolith",
                    "bowl of rice",
                    "ornament",
                    "shell",
                    "astrolabe",
                    "planetoid",
                    "fuel cell",
                    "cake"
                };
                for (int combo = 0; combo < (1 << items.Length); combo++)
                {
                    System.Collections.Specialized.BitVector32 comboBits = new(combo);
                    //Console.WriteLine($"(Combo {comboBits})");
                    for (int index = 0; index < items.Length; index++)
                    {
                        string prefix = (comboBits[1 << index]) ? "take " : "drop ";
                        string cmd = prefix + items[index] + "\n";
                        foreach (char c in cmd)
                        {
                            yield return (BigInteger)c;
                        }
                    }
                    foreach (char c in "inv\nnorth\n")
                    {
                        yield return (BigInteger)c;
                    }
                    var location = Regex.Match(log.ToString(), "==.+==", RegexOptions.RightToLeft);
                    if (location.Value != "== Security Checkpoint ==")
                    {
                        break;
                    }
                }
                Console.WriteLine("(Interactive mode start!)");
                foreach (var c in manualInputs())
                {
                    yield return c;
                }
            }
            const bool MANUAL_MODE = false;
            IEnumerable<BigInteger> inputs = MANUAL_MODE ? manualInputs() : automaticInputs();
            foreach (var c in interpreter.RunToEnd(inputs))
            {
                if (c <= 0xFF)
                {
                    if (MANUAL_MODE) Console.Write((char)c);
                    log.Append((char)c);
                }
                else
                {
                    if (MANUAL_MODE) Console.WriteLine(c);
                    log.AppendLine(c.ToString());
                }
            }
            var password = Regex.Match(log.ToString(), @"\d+", RegexOptions.RightToLeft);
            return password.Value;
        }

        public string Part2()
        {
            return "Merry Christmas!";
        }
    }
}
