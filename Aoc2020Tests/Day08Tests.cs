namespace Aoc2020.Tests
{
    // https://adventofcode.com/2020/day/8
    // --- Day 8: Handheld Halting ---
    [TestClass()]
    public class Day08Tests
    {
        [TestMethod()]
        public void Day08_Part1_ExampleTest()
        {
            var instance = new Day08(File.ReadAllText("inputs/day08-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("5", answer);
        }
        [TestMethod()]
        public void Day08_Part1_InputTest()
        {
            var instance = new Day08(File.ReadAllText("inputs/day08-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("1928", answer);
        }

        [TestMethod()]
        public void Day08_Part2_ExampleTest()
        {
            var instance = new Day08(File.ReadAllText("inputs/day08-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("8", answer);
        }
        [TestMethod()]
        public void Day08_Part2_InputTest()
        {
            var instance = new Day08(File.ReadAllText("inputs/day08-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("1319", answer);
        }
    }
}