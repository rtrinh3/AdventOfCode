using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2019.Tests
{
    [TestClass()]
    public class Day23Tests
    {
        [TestMethod(), Timeout(5000)]
        public void Part1InputTest()
        {
            var instance = new Day23(File.ReadAllText("day23-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("24268", answer);
        }

        [TestMethod(), Timeout(5000)]
        public void Part2InputTest()
        {
            var instance = new Day23(File.ReadAllText("day23-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("19316", answer);
        }
    }
}