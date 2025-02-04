namespace Aoc2015.Tests
{
    // https://adventofcode.com/2015/day/13
    // --- Day 13: Knights of the Dinner Table ---
    [TestClass()]
    public class Day13Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day13(File.ReadAllText("day13-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("330", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day13(File.ReadAllText("day13-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("733", answer);
        }

        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day13(File.ReadAllText("day13-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("725", answer);
        }
    }
}