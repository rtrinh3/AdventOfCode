using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2019.Tests
{
    [TestClass()]
    public class Day07Tests
    {
        [TestMethod()]
        public void Part1Example1Test()
        {
            var instance = new Day07(File.ReadAllText("inputs/day07-example1.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("43210", answer);
        }
        [TestMethod()]
        public void Part1Example2Test()
        {
            var instance = new Day07(File.ReadAllText("inputs/day07-example2.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("54321", answer);
        }
        [TestMethod()]
        public void Part1Example3Test()
        {
            var instance = new Day07(File.ReadAllText("inputs/day07-example3.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("65210", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day07(File.ReadAllText("inputs/day07-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("30940", answer);
        }

        [TestMethod()]
        public void Part2Example4Test()
        {
            var instance = new Day07(File.ReadAllText("inputs/day07-example4.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("139629729", answer);
        }
        [TestMethod()]
        public void Part2Example5Test()
        {
            var instance = new Day07(File.ReadAllText("inputs/day07-example5.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("18216", answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day07(File.ReadAllText("inputs/day07-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("76211147", answer);
        }
    }
}