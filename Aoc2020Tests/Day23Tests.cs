namespace Aoc2020.Tests
{
    // https://adventofcode.com/2020/day/23
    // --- Day 23: Crab Cups ---
    [TestClass()]
    public class Day23Tests
    {
        [TestMethod()]
        public void Day23_Part1_ExampleTest()
        {
            var instance = new Day23(File.ReadAllText("inputs/day23-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("67384529", answer);
        }
        [TestMethod()]
        public void Day23_Part1_InputTest()
        {
            var instance = new Day23(File.ReadAllText("inputs/day23-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("36542897", answer);
        }

        [TestMethod()]
        public void Day23_Part2_ExampleTest()
        {
            var instance = new Day23(File.ReadAllText("inputs/day23-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("149245887792", answer);
        }
        [TestMethod()]
        public void Day23_Part2_InputTest()
        {
            var instance = new Day23(File.ReadAllText("inputs/day23-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("562136730660", answer);
        }
    }
}