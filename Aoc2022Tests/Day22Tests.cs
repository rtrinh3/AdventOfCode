using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2022.Tests
{
    [TestClass()]
    public class Day22Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day22(File.ReadAllText("day22-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("6032", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day22(File.ReadAllText("day22-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("31568", answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day22(File.ReadAllText("day22-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("5031", answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day22(File.ReadAllText("day22-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("36540", answer);
        }
    }
}