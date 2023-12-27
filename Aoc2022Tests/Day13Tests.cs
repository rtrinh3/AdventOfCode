using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2022.Tests
{
    [TestClass()]
    public class Day13Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day13(File.ReadAllText("day13-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("13", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day13(File.ReadAllText("day13-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("6086", answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day13(File.ReadAllText("day13-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("140", answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day13(File.ReadAllText("day13-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("27930", answer);
        }
    }
}