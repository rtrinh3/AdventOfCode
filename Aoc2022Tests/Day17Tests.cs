using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2022.Tests
{
    [TestClass()]
    public class Day17Tests
    {
        [TestMethod(), Timeout(1000)]
        public void Part1ExampleTest()
        {
            var instance = new Day17(File.ReadAllText("day17-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("3068", answer);
        }
        [TestMethod(), Timeout(2000)]
        public void Part1InputTest()
        {
            var instance = new Day17(File.ReadAllText("day17-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("3135", answer);
        }

        [TestMethod(), Timeout(1000)]
        public void Part2ExampleTest()
        {
            var instance = new Day17(File.ReadAllText("day17-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("1514285714288", answer);
        }
        [TestMethod(), Timeout(1000)]
        public void Part2InputTest()
        {
            var instance = new Day17(File.ReadAllText("day17-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("1569054441243", answer);
        }
    }
}