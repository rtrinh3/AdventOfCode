using Aoc2024;

namespace Aoc2024.Tests
{
    // https://adventofcode.com/2024/day/1
    // Day 1: Historian Hysteria
    [TestClass()]
    public class Day01Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day01(File.ReadAllText("day01-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("11", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day01(File.ReadAllText("day01-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("2378066", answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day01(File.ReadAllText("day01-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("31", answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day01(File.ReadAllText("day01-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("18934359", answer);
        }
    }
}