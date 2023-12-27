using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2022.Tests
{
    [TestClass()]
    public class Day19Tests
    {
        [TestMethod(), Timeout(5_000)]
        public void Part1Example1Test()
        {
            var instance = new Day19(File.ReadAllText("day19-example.txt"));
            var answer = instance.EvaluateBlueprint(0, 24);
            Assert.AreEqual(9, answer);
        }
        [TestMethod(), Timeout(5_000)]
        public void Part1Example2Test()
        {
            var instance = new Day19(File.ReadAllText("day19-example.txt"));
            var answer = instance.EvaluateBlueprint(1, 24);
            Assert.AreEqual(12, answer);
        }
        [TestMethod(), Timeout(5_000)]
        public void Part1FullExampleTest()
        {
            var instance = new Day19(File.ReadAllText("day19-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("33", answer);
        }
        [TestMethod(), Timeout(25_000)]
        public void Part1InputTest()
        {
            var instance = new Day19(File.ReadAllText("day19-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("1962", answer);
        }

        [TestMethod(), Timeout(15_000)]
        public void Part2Example1Test()
        {
            var instance = new Day19(File.ReadAllText("day19-example.txt"));
            var answer = instance.EvaluateBlueprint(0, 32);
            Assert.AreEqual(56, answer);
        }
        [TestMethod(), Timeout(5_000)]
        public void Part2Example2Test()
        {
            var instance = new Day19(File.ReadAllText("day19-example.txt"));
            var answer = instance.EvaluateBlueprint(1, 32);
            Assert.AreEqual(62, answer);
        }
        [TestMethod(), Timeout(15_000)]
        public void Part2InputTest()
        {
            var instance = new Day19(File.ReadAllText("day19-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("88160", answer);
        }
    }
}