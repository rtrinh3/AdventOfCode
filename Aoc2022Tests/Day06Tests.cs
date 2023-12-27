using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2022.Tests
{
    [TestClass()]
    public class Day06Tests
    {
        [TestMethod()]
        public void Part1Example1Test()
        {
            var instance = new Day06(File.ReadAllText("day06-example1.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("7", answer);
        }
        [TestMethod()]
        public void Part1Example2Test()
        {
            var instance = new Day06(File.ReadAllText("day06-example2.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("5", answer);
        }
        [TestMethod()]
        public void Part1Example3Test()
        {
            var instance = new Day06(File.ReadAllText("day06-example3.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("6", answer);
        }
        [TestMethod()]
        public void Part1Example4Test()
        {
            var instance = new Day06(File.ReadAllText("day06-example4.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("10", answer);
        }
        [TestMethod()]
        public void Part1Example5Test()
        {
            var instance = new Day06(File.ReadAllText("day06-example5.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("11", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day06(File.ReadAllText("day06-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("1198", answer);
        }

        [TestMethod()]
        public void Part2Example1Test()
        {
            var instance = new Day06(File.ReadAllText("day06-example1.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("19", answer);
        }
        [TestMethod()]
        public void Part2Example2Test()
        {
            var instance = new Day06(File.ReadAllText("day06-example2.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("23", answer);
        }
        [TestMethod()]
        public void Part2Example3Test()
        {
            var instance = new Day06(File.ReadAllText("day06-example3.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("23", answer);
        }
        [TestMethod()]
        public void Part2Example4Test()
        {
            var instance = new Day06(File.ReadAllText("day06-example4.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("29", answer);
        }
        [TestMethod()]
        public void Part2Example5Test()
        {
            var instance = new Day06(File.ReadAllText("day06-example5.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("26", answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day06(File.ReadAllText("day06-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("3120", answer);
        }
    }
}