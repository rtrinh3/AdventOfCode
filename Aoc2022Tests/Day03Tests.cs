using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2022.Tests
{
    [TestClass()]
    public class Day03Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day03(File.ReadAllText("day03-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("157", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day03(File.ReadAllText("day03-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("7872", answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day03(File.ReadAllText("day03-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("70", answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day03(File.ReadAllText("day03-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("2497", answer);
        }
    }
}