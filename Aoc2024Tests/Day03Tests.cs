namespace Aoc2024.Tests
{
    // https://adventofcode.com/2024/day/3
    // --- Day 3: Mull It Over ---
    [TestClass()]
    public class Day03Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day03(File.ReadAllText("day03-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("161", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day03(File.ReadAllText("day03-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("188192787", answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day03(File.ReadAllText("day03-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("48", answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day03(File.ReadAllText("day03-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("113965544", answer);
        }
    }
}