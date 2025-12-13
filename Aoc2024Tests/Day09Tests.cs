namespace Aoc2024.Tests
{
    // https://adventofcode.com/2024/day/9
    // --- Day 9: Disk Fragmenter ---
    [TestClass()]
    public class Day09Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day09(File.ReadAllText("inputs/day09-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("1928", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day09(File.ReadAllText("inputs/day09-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("6241633730082", answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day09(File.ReadAllText("inputs/day09-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("2858", answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day09(File.ReadAllText("inputs/day09-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("6265268809555", answer);
        }
    }
}