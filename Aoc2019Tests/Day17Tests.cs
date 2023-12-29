using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2019.Tests
{
    [TestClass()]
    public class Day17Tests
    {
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day17(File.ReadAllText("day17-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("6052", answer);
        }

        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day17(File.ReadAllText("day17-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("752491", answer);
        }
    }
}