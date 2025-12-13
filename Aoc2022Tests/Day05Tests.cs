using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2022.Tests
{
    [TestClass()]
    public class Day05Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day05(File.ReadAllText("inputs/day05-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("CMZ", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day05(File.ReadAllText("inputs/day05-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("VRWBSFZWM", answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day05(File.ReadAllText("inputs/day05-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("MCD", answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day05(File.ReadAllText("inputs/day05-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("RBTWJWMCF", answer);
        }
    }
}