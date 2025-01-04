using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2019.Tests
{
    [TestClass()]
    public class Day18Tests
    {
        [TestMethod()]
        public void Part1Example1Test()
        {
            var instance = new Day18(File.ReadAllText("day18-example1.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("86", answer);
        }
        [TestMethod()]
        public void Part1Example2Test()
        {
            var instance = new Day18(File.ReadAllText("day18-example2.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("132", answer);
        }
        [TestMethod()]
        public void Part1Example3Test()
        {
            var instance = new Day18(File.ReadAllText("day18-example3.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("136", answer);
        }
        [TestMethod()]
        public void Part1Example4Test()
        {
            var instance = new Day18(File.ReadAllText("day18-example4.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("81", answer);
        }
        [TestMethod(), Timeout(10_000)]
        public void Part1InputTest()
        {
            var instance = new Day18(File.ReadAllText("day18-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("2684", answer);
        }

        [TestMethod()]
        public void Part2Example5Test()
        {
            var instance = new Day18(File.ReadAllText("day18-example5.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("8", answer);
        }
        [TestMethod()]
        public void Part2Example6Test()
        {
            var instance = new Day18(File.ReadAllText("day18-example6.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("24", answer);
        }
        [TestMethod()]
        public void Part2Example7Test()
        {
            var instance = new Day18(File.ReadAllText("day18-example7.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("32", answer);
        }
        [TestMethod()]
        public void Part2Example8Test()
        {
            var instance = new Day18(File.ReadAllText("day18-example8.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("72", answer);
        }
        [TestMethod(), Timeout(10_000)]
        public void Part2InputTest()
        {
            var instance = new Day18(File.ReadAllText("day18-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("1886", answer);
        }
    }
}