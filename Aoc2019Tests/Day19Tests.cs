using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2019.Tests
{
    [TestClass()]
    public class Day19Tests
    {
        [TestMethod(), Timeout(2000)]
        public void Part1InputTest()
        {
            var instance = new Day19(File.ReadAllText("day19-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("199", answer);
        }

        [TestMethod(), Timeout(15_000)]
        public void Part2InputTest()
        {
            var instance = new Day19(File.ReadAllText("day19-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("10180726", answer);
        }
    }
}