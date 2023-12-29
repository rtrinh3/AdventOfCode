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
            Assert.AreEqual("", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day24(File.ReadAllText("day24-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("", answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day24(File.ReadAllText("day24-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("", answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day24(File.ReadAllText("day24-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("", answer);
        }
    }
}