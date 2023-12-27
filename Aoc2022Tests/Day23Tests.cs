using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2022.Tests
{
    [TestClass()]
    public class Day23Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day23(File.ReadAllText("day23-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("110", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day23(File.ReadAllText("day23-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("4056", answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day23(File.ReadAllText("day23-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("20", answer);
        }
        [TestMethod(), Timeout(5000)]
        public void Part2InputTest()
        {
            var instance = new Day23(File.ReadAllText("day23-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("999", answer);
        }
    }
}