using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2022.Tests
{
    [TestClass()]
    public class Day04Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day04(File.ReadAllText("inputs/day04-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("2", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day04(File.ReadAllText("inputs/day04-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("503", answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day04(File.ReadAllText("inputs/day04-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("4", answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day04(File.ReadAllText("inputs/day04-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("827", answer);
        }
    }
}