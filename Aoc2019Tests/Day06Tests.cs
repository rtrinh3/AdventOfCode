using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2019.Tests
{
    [TestClass()]
    public class Day06Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day06(File.ReadAllText("inputs/day06-example1.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("42", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day06(File.ReadAllText("inputs/day06-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("308790", answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day06(File.ReadAllText("inputs/day06-example2.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("4", answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day06(File.ReadAllText("inputs/day06-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("472", answer);
        }
    }
}