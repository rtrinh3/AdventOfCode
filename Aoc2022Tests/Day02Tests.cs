using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2022.Tests
{
    [TestClass()]
    public class Day02Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day02(File.ReadAllText("inputs/day02-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("15", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day02(File.ReadAllText("inputs/day02-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("11063", answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day02(File.ReadAllText("inputs/day02-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("12", answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day02(File.ReadAllText("inputs/day02-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("10349", answer);
        }
    }
}