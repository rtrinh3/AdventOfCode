using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2019.Tests
{
    [TestClass()]
    public class Day12Tests
    {
        [TestMethod()]
        public void Part1Example1Test()
        {
            var instance = new Day12(File.ReadAllText("day12-example1.txt"));
            var answer = instance.DoPart1(10);
            Assert.AreEqual(179, answer);
        }
        [TestMethod()]
        public void Part1Example2Test()
        {
            var instance = new Day12(File.ReadAllText("day12-example2.txt"));
            var answer = instance.DoPart1(100);
            Assert.AreEqual(1940, answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day12(File.ReadAllText("day12-input.txt"));
            var answer = instance.DoPart1(1000);
            Assert.AreEqual(8454, answer);
        }

        [TestMethod()]
        public void Part2Example1Test()
        {
            var instance = new Day12(File.ReadAllText("day12-example1.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("2772", answer);
        }
        [TestMethod()]
        public void Part2Example2Test()
        {
            var instance = new Day12(File.ReadAllText("day12-example2.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("4686774924", answer);
        }
        [TestMethod(), Timeout(5000)]
        public void Part2InputTest()
        {
            var instance = new Day12(File.ReadAllText("day12-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("362336016722948", answer);
        }
    }
}