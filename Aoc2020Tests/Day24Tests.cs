namespace Aoc2020.Tests
{
    // https://adventofcode.com/2020/day/24
    // --- Day 24: Lobby Layout ---
    [TestClass()]
    public class Day24Tests
    {
        [TestMethod()]
        public void Day24_Part1_ExampleTest()
        {
            var instance = new Day24(File.ReadAllText("day24-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("10", answer);
        }
        [TestMethod()]
        public void Day24_Part1_InputTest()
        {
            var instance = new Day24(File.ReadAllText("day24-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("459", answer);
        }

        [TestMethod()]
        public void Day24_Part2_ExampleTest()
        {
            var instance = new Day24(File.ReadAllText("day24-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("2208", answer);
        }
        [TestMethod()]
        public void Day24_Part2_InputTest()
        {
            var instance = new Day24(File.ReadAllText("day24-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("4150", answer);
        }
    }
}