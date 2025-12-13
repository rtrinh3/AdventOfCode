using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2019.Tests
{
    [TestClass()]
    public class Day14Tests
    {
        [TestMethod()]
        public void Part1Example1Test()
        {
            var instance = new Day14(File.ReadAllText("inputs/day14-example1.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("31", answer);
        }
        [TestMethod()]
        public void Part1Example2Test()
        {
            var instance = new Day14(File.ReadAllText("inputs/day14-example2.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("165", answer);
        }
        [TestMethod()]
        public void Part1Example3Test()
        {
            var instance = new Day14(File.ReadAllText("inputs/day14-example3.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("13312", answer);
        }
        [TestMethod()]
        public void Part1Example4Test()
        {
            var instance = new Day14(File.ReadAllText("inputs/day14-example4.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("180697", answer);
        }
        [TestMethod()]
        public void Part1Example5Test()
        {
            var instance = new Day14(File.ReadAllText("inputs/day14-example5.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("2210736", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day14(File.ReadAllText("inputs/day14-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("522031", answer);
        }

        [TestMethod()]
        public void Part2Example3Test()
        {
            var instance = new Day14(File.ReadAllText("inputs/day14-example3.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("82892753", answer);
        }
        [TestMethod()]
        public void Part2Example4Test()
        {
            var instance = new Day14(File.ReadAllText("inputs/day14-example4.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("5586022", answer);
        }
        [TestMethod()]
        public void Part2Example5Test()
        {
            var instance = new Day14(File.ReadAllText("inputs/day14-example5.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("460664", answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day14(File.ReadAllText("inputs/day14-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("3566577", answer);
        }
    }
}