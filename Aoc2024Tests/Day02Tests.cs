namespace Aoc2024.Tests
{
    // https://adventofcode.com/2024/day/2
    // --- Day 2: Red-Nosed Reports ---
    [TestClass()]
    public class Day02Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day02(File.ReadAllText("day02-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("2", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day02(File.ReadAllText("day02-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("282", answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day02(File.ReadAllText("day02-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("4", answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day02(File.ReadAllText("day02-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("349", answer);
        }
    }
}