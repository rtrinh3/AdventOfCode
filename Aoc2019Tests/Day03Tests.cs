using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2019.Tests
{
    [TestClass()]
    public class Day03Tests
    {
        [TestMethod()]
        public void Part1Example1Test()
        {
            var instance = new Day03(File.ReadAllText("day03-example1.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("6", answer);
        }
        [TestMethod()]
        public void Part1Example2Test()
        {
            var instance = new Day03(File.ReadAllText("day03-example2.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("159", answer);
        }
        [TestMethod()]
        public void Part1Example3Test()
        {
            var instance = new Day03(File.ReadAllText("day03-example3.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("135", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day03(File.ReadAllText("day03-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("258", answer);
        }

        [TestMethod()]
        public void Part2Example1Test()
        {
            var instance = new Day03(File.ReadAllText("day03-example1.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("30", answer);
        }
        [TestMethod()]
        public void Part2Example2Test()
        {
            var instance = new Day03(File.ReadAllText("day03-example2.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("610", answer);
        }
        [TestMethod()]
        public void Part2Example3Test()
        {
            var instance = new Day03(File.ReadAllText("day03-example3.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("410", answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day03(File.ReadAllText("day03-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("12304", answer);
        }
    }
}