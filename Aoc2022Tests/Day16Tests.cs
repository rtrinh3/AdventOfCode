using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2022.Tests
{
    [TestClass()]
    public class Day16Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day16(File.ReadAllText("day16-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("1651", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day16(File.ReadAllText("day16-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("1871", answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day16(File.ReadAllText("day16-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("1707", answer);
        }
        [TestMethod(), Timeout(10_000)]
        public void Part2InputTest()
        {
            var instance = new Day16(File.ReadAllText("day16-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("2416", answer);
        }
    }
}