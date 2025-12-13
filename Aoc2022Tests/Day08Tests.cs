using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2022.Tests
{
    [TestClass()]
    public class Day08Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day08(File.ReadAllText("inputs/day08-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("21", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day08(File.ReadAllText("inputs/day08-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("1812", answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day08(File.ReadAllText("inputs/day08-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("8", answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day08(File.ReadAllText("inputs/day08-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("315495", answer);
        }
    }
}