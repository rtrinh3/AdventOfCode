namespace Aoc2024.Tests
{
    // https://adventofcode.com/2024/day/7
    // --- Day 7: Bridge Repair ---
    [TestClass()]
    public class Day07Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day07(File.ReadAllText("day07-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("3749", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day07(File.ReadAllText("day07-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("1620690235709", answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day07(File.ReadAllText("day07-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("11387", answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day07(File.ReadAllText("day07-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("145397611075341", answer);
        }
    }
}