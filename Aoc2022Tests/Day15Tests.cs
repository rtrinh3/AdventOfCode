using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2022.Tests
{
    [TestClass()]
    public class Day15Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day15(File.ReadAllText("inputs/day15-example.txt"));
            var answer = instance.DoPart1(10);
            Assert.AreEqual(26, answer);
        }
        [TestMethod(), Timeout(10_000)]
        public void Part1InputTest()
        {
            var instance = new Day15(File.ReadAllText("inputs/day15-input.txt"));
            var answer = instance.DoPart1(2000000);
            Assert.AreEqual(4873353, answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day15(File.ReadAllText("inputs/day15-example.txt"));
            var answer = instance.DoPart2(20, 20);
            Assert.AreEqual(56000011, answer);
        }
        [TestMethod(), Timeout(10_000)]
        public void Part2InputTest()
        {
            var instance = new Day15(File.ReadAllText("inputs/day15-input.txt"));
            var answer = instance.DoPart2(4000000, 4000000);
            Assert.AreEqual(11600823139120, answer);
        }
    }
}