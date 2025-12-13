using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2019.Tests
{
    [TestClass()]
    public class Day05Tests
    {
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day05(File.ReadAllText("inputs/day05-input.txt"));
            var answer = instance.Part1();
            var outputs = answer.Split('\n');
            Assert.IsTrue(outputs.SkipLast(1).All(x => x == "0"));
            Assert.AreEqual("5182797", outputs.Last());
        }

        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day05(File.ReadAllText("inputs/day05-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("12077198", answer);
        }
    }
}