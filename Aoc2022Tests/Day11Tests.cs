using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2022.Tests
{
    [TestClass()]
    public class Day11Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day11(File.ReadAllText("inputs/day11-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("10605", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day11(File.ReadAllText("inputs/day11-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("61005", answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day11(File.ReadAllText("inputs/day11-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("2713310158", answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day11(File.ReadAllText("inputs/day11-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("20567144694", answer);
        }
    }
}