namespace Aoc2024.Tests
{
    // https://adventofcode.com/2024/day/4
    // --- Day 4: Ceres Search ---
    [TestClass()]
    public class Day04Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day04(File.ReadAllText("inputs/day04-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("18", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day04(File.ReadAllText("inputs/day04-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("2560", answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day04(File.ReadAllText("inputs/day04-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("9", answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day04(File.ReadAllText("inputs/day04-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("1910", answer);
        }
    }
}