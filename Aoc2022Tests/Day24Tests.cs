using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2022.Tests
{
    [TestClass()]
    public class Day24Tests
    {
        [TestMethod(), Timeout(10_000)]
        public void Part1ExampleTest()
        {
            var instance = new Day24(File.ReadAllText("inputs/day24-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("18", answer);
        }
        [TestMethod(), Timeout(10_000)]
        public void Part1InputTest()
        {
            var instance = new Day24(File.ReadAllText("inputs/day24-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("297", answer);
        }

        [TestMethod(), Timeout(10_000)]
        public void Part2ExampleTest()
        {
            var instance = new Day24(File.ReadAllText("inputs/day24-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("54", answer);
        }
        [TestMethod(), Timeout(10_000)]
        public void Part2InputTest()
        {
            var instance = new Day24(File.ReadAllText("inputs/day24-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("856", answer);
        }
    }
}