using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2019.Tests
{
    [TestClass()]
    public class Day11Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day11(File.ReadAllText("day11-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day11(File.ReadAllText("day11-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("", answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day11(File.ReadAllText("day11-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("", answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day11(File.ReadAllText("day11-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("", answer);
        }
    }
}