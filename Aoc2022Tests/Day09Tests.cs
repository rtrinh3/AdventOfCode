using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2022.Tests
{
    [TestClass()]
    public class Day09Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day09(File.ReadAllText("day09-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("13", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day09(File.ReadAllText("day09-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("6339", answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day09(File.ReadAllText("day09-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("36", answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day09(File.ReadAllText("day09-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("2541", answer);
        }
    }
}