namespace Aoc2024.Tests
{
    // https://adventofcode.com/2024/day/6
    // --- Day 6: Guard Gallivant ---
    [TestClass()]
    public class Day06Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day06(File.ReadAllText("day06-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("41", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day06(File.ReadAllText("day06-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("4982", answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day06(File.ReadAllText("day06-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("6", answer);
        }
        [TestMethod(), Timeout(60_000)]
        public void Part2InputTest()
        {
            var instance = new Day06(File.ReadAllText("day06-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("1663", answer);
        }
    }
}