namespace Aoc2024.Tests
{
    // https://adventofcode.com/2024/day/14
    // --- Day 14: Restroom Redoubt ---
    [TestClass()]
    public class Day14Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day14(File.ReadAllText("day14-example.txt"));
            var answer = instance.DoPart1(11, 7);
            Assert.AreEqual(12, answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day14(File.ReadAllText("day14-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("208437768", answer);
        }

        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day14(File.ReadAllText("day14-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("7492", answer);
        }
    }
}