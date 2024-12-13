namespace Aoc2024.Tests
{
    // https://adventofcode.com/2024/day/10
    // --- Day 10: Hoof It ---
    [TestClass()]
    public class Day10Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day10(File.ReadAllText("day10-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("36", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day10(File.ReadAllText("day10-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("430", answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day10(File.ReadAllText("day10-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("81", answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day10(File.ReadAllText("day10-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("928", answer);
        }
    }
}