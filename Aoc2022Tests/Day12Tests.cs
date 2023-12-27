using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2022.Tests
{
    [TestClass()]
    public class Day12Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day12(File.ReadAllText("day12-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("31", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day12(File.ReadAllText("day12-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("481", answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day12(File.ReadAllText("day12-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("29", answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day12(File.ReadAllText("day12-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("480", answer);
        }
    }
}