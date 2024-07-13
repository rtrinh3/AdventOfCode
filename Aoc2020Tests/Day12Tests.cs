namespace Aoc2020.Tests
{
    // https://adventofcode.com/2020/day/12
    // --- Day 12: Rain Risk ---
    [TestClass()]
    public class Day12Tests
    {
        [TestMethod()]
        public void Day12_Part1_ExampleTest()
        {
            var instance = new Day12(File.ReadAllText("day12-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("25", answer);
        }
        [TestMethod()]
        public void Day12_Part1_InputTest()
        {
            var instance = new Day12(File.ReadAllText("day12-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("1319", answer);
        }

        [TestMethod()]
        public void Day12_Part2_ExampleTest()
        {
            var instance = new Day12(File.ReadAllText("day12-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("286", answer);
        }
        [TestMethod()]
        public void Day12_Part2_InputTest()
        {
            var instance = new Day12(File.ReadAllText("day12-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("62434", answer);
        }
    }
}