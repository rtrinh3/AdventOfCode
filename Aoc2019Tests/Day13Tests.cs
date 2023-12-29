using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2019.Tests
{
    [TestClass()]
    public class Day13Tests
    {
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day13(File.ReadAllText("day13-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("372", answer);
        }

        [TestMethod(), Timeout(5000)]
        public void Part2InputTest()
        {
            var instance = new Day13(File.ReadAllText("day13-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("19297", answer);
        }
    }
}