namespace Aoc2015.Tests
{
    // https://adventofcode.com/2015/day/19
    // --- Day 19: Medicine for Rudolph ---
    [TestClass()]
    public class Day19Tests
    {
        [TestMethod()]
        public void Part1Example1Test()
        {
            var instance = new Day19(File.ReadAllText("day19-example1.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("4", answer);
        }
        [TestMethod()]
        public void Part1Example2Test()
        {
            var instance = new Day19(File.ReadAllText("day19-example2.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("7", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day19(File.ReadAllText("day19-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("518", answer);
        }

        [TestMethod()]
        public void Part2Example1Test()
        {
            var instance = new Day19(File.ReadAllText("day19-example1.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("3", answer);
        }
        [TestMethod()]
        public void Part2Example2Test()
        {
            var instance = new Day19(File.ReadAllText("day19-example2.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("6", answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day19(File.ReadAllText("day19-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("200", answer);
        }
    }
}
