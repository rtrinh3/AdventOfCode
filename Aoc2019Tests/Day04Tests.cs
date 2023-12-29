using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2019.Tests
{
    [TestClass()]
    public class Day04Tests
    {
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day04(File.ReadAllText("day04-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("925", answer);
        }

        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day04(File.ReadAllText("day04-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("607", answer);
        }
    }
}