using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2019.Tests
{
    [TestClass()]
    public class Day10Tests
    {
        [TestMethod()]
        public void Part1Example1Test()
        {
            var instance = new Day10(File.ReadAllText("day10-example1.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("8", answer);
        }
        [TestMethod()]
        public void Part1Example2Test()
        {
            var instance = new Day10(File.ReadAllText("day10-example2.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("33", answer);
        }
        [TestMethod()]
        public void Part1Example3Test()
        {
            var instance = new Day10(File.ReadAllText("day10-example3.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("35", answer);
        }
        [TestMethod()]
        public void Part1Example4Test()
        {
            var instance = new Day10(File.ReadAllText("day10-example4.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("41", answer);
        }
        [TestMethod()]
        public void Part1Example5Test()
        {
            var instance = new Day10(File.ReadAllText("day10-example5.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("210", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day10(File.ReadAllText("day10-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("247", answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day10(File.ReadAllText("day10-example5.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("802", answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day10(File.ReadAllText("day10-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("1919", answer);
        }
    }
}