using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2022.Tests
{
    [TestClass()]
    public class Day10Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day10(File.ReadAllText("day10-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("13140", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day10(File.ReadAllText("day10-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("15120", answer);
        }

        //[TestMethod()]
        //public void Part2ExampleTest()
        //{
        //    var instance = new Day10(File.ReadAllText("day10-example.txt"));
        //    var answer = instance.Part2();
        //    Assert.AreEqual("45000", answer);
        //}
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day10(File.ReadAllText("day10-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("RKPJBPLA", answer);
        }
    }
}