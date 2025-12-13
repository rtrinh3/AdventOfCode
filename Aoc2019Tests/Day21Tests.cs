using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2019.Tests
{
    [TestClass()]
    public class Day21Tests
    {
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day21(File.ReadAllText("inputs/day21-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("19355645", answer);
        }

        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day21(File.ReadAllText("inputs/day21-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("1137899149", answer);
        }
    }
}