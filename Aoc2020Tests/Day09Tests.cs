namespace Aoc2020.Tests
{
    // https://adventofcode.com/2020/day/9
    // --- Day 9: Encoding Error ---
    [TestClass()]
    public class Day09Tests
    {
        private const int EXAMPLE_PREAMBLE_LENGTH = 5;

        [TestMethod()]
        public void Day09_Part1_ExampleTest()
        {
            var instance = new Day09(File.ReadAllText("day09-example.txt"));
            var answer = instance.DoPart1(EXAMPLE_PREAMBLE_LENGTH);
            Assert.AreEqual(127, answer);
        }
        [TestMethod()]
        public void Day09_Part1_InputTest()
        {
            var instance = new Day09(File.ReadAllText("day09-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(1930745883, answer);
        }

        [TestMethod()]
        public void Day09_Part2_ExampleTest()
        {
            var instance = new Day09(File.ReadAllText("day09-example.txt"));
            var answer = instance.DoPart2(EXAMPLE_PREAMBLE_LENGTH);
            Assert.AreEqual(62, answer);
        }
        [TestMethod()]
        public void Day09_Part2_InputTest()
        {
            var instance = new Day09(File.ReadAllText("day09-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(268878261, answer);
        }
    }
}