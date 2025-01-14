using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2019.Tests
{
    [TestClass()]
    public class Day24Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day24(File.ReadAllText("day24-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("2129920", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day24(File.ReadAllText("day24-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("32506911", answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day24(File.ReadAllText("day24-example.txt"));
            var answer = instance.DoPart2(10);
            Assert.AreEqual(99, answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day24(File.ReadAllText("day24-input.txt"));
            var answer = instance.DoPart2(200);
            Assert.AreEqual(2025, answer);
        }
    }
}