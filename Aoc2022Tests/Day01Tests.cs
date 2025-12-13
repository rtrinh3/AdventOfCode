using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2022.Tests
{
    [TestClass()]
    public class Day01Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day01(File.ReadAllText("inputs/day01-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("24000", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day01(File.ReadAllText("inputs/day01-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("75501", answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day01(File.ReadAllText("inputs/day01-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("45000", answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day01(File.ReadAllText("inputs/day01-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("215594", answer);
        }
    }
}