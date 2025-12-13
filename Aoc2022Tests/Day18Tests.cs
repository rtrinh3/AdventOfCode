using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2022.Tests
{
    [TestClass()]
    public class Day18Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day18(File.ReadAllText("inputs/day18-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("64", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day18(File.ReadAllText("inputs/day18-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("4456", answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day18(File.ReadAllText("inputs/day18-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("58", answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day18(File.ReadAllText("inputs/day18-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("2510", answer);
        }
    }
}