using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2019.Tests
{
    [TestClass()]
    public class Day20Tests
    {
        [TestMethod()]
        public void Part1Example1Test()
        {
            var instance = new Day20(File.ReadAllText("day20-example1.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("23", answer);
        }
        [TestMethod()]
        public void Part1Example2Test()
        {
            var instance = new Day20(File.ReadAllText("day20-example2.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("58", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day20(File.ReadAllText("day20-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("638", answer);
        }

        [TestMethod()]
        public void Part2Example3Test()
        {
            var instance = new Day20(File.ReadAllText("day20-example3.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("396", answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day20(File.ReadAllText("day20-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("7844", answer);
        }
    }
}