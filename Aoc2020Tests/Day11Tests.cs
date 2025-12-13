namespace Aoc2020.Tests
{
    // https://adventofcode.com/2020/day/11
    // --- Day 11: Seating System ---
    [TestClass()]
    public class Day11Tests
    {
        [TestMethod()]
        public void Day11_Part1_ExampleTest()
        {
            var instance = new Day11(File.ReadAllText("inputs/day11-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("37", answer);
        }
        [TestMethod()]
        public void Day11_Part1_InputTest()
        {
            var instance = new Day11(File.ReadAllText("inputs/day11-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("2483", answer);
        }

        [TestMethod()]
        public void Day11_Part2_ExampleTest()
        {
            var instance = new Day11(File.ReadAllText("inputs/day11-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("26", answer);
        }
        [TestMethod()]
        public void Day11_Part2_InputTest()
        {
            var instance = new Day11(File.ReadAllText("inputs/day11-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("2285", answer);
        }
    }
}