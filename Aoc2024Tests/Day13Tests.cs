using System.Text.RegularExpressions;

namespace Aoc2024.Tests
{
    // https://adventofcode.com/2024/day/13
    // --- Day 13: Claw Contraption ---
    [TestClass()]
    public class Day13Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day13(File.ReadAllText("day13-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("480", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day13(File.ReadAllText("day13-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("37128", answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var example = File.ReadAllText("day13-example.txt");
            var paragraphs = example.TrimEnd().ReplaceLineEndings("\n").Split("\n\n");
            Assert.AreEqual(paragraphs.Length, 4);
            for (int i = 0; i < paragraphs.Length; i++)
            {
                var instance = new Day13(paragraphs[i]);
                var answer = instance.Part2();
                // Only the 2nd and the 4th are winnable
                if (i == 1 || i == 3)
                {
                    // It'll take more than 100 tokens to win
                    Assert.IsTrue(long.Parse(answer) > 100);
                }
                else
                {
                    Assert.IsTrue(long.Parse(answer) <= 0);
                }
            }
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day13(File.ReadAllText("day13-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("74914228471331", answer);
        }
    }
}