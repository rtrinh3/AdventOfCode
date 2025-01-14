using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2022.Tests
{
    [TestClass()]
    public class Day22Tests
    {
        [TestMethod(), Timeout(10_000)]
        public void Part1ExampleTest()
        {
            var instance = new Day22(File.ReadAllText("day22-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("6032", answer);
        }
        [TestMethod(), Timeout(10_000)]
        public void Part1InputTest()
        {
            var instance = new Day22(File.ReadAllText("day22-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("31568", answer);
        }

        [TestMethod(), Timeout(10_000)]
        public void Part2ExampleTest()
        {
            var instance = new Day22(File.ReadAllText("day22-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("5031", answer);
        }
        [TestMethod(), Timeout(10_000)]
        public void Part2InputTest()
        {
            var instance = new Day22(File.ReadAllText("day22-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("36540", answer);
        }
        [TestMethod(), Timeout(10_000)]
        public void Part2RedditTest()
        {
            // https://www.reddit.com/r/adventofcode/comments/zuso8x/2022_day_22_part_3/
            string inputs = File.ReadAllText("day22-reddit.txt");
            string[] paragraphs = inputs.ReplaceLineEndings("\n").Split("\n\n");
            string[] puzzles = paragraphs.Chunk(2).Select(parts => parts[0] + "\n\n" + parts[1]).ToArray();
            long[] expected = [165227, 151196, 179352, 53386, 65169, 92197, 77907, 99465, 45132, 67400, 52550, 32121, 23366, 95688, 55264, 174214, 132159, 33022, 97381, 171197, 9364, 25321, 129406, 79694, 99413, 79369, 63803, 66090];
            Assert.AreEqual(expected.Length, puzzles.Length);
            long sum = 0;
            for (int i = 0; i < puzzles.Length; i++)
            {
                var instance = new Day22(puzzles[i]);
                var answer = instance.Part2();
                var answerInt = long.Parse(answer);
                Assert.AreEqual(expected[i], answerInt);
                sum += answerInt;
            }
            Assert.AreEqual(2415853, sum);
        }
    }
}