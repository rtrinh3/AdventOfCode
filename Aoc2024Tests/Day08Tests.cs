namespace Aoc2024.Tests
{
    // https://adventofcode.com/2024/day/8
    // --- Day 8: Resonant Collinearity ---
    [TestClass()]
    public class Day08Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day08(File.ReadAllText("inputs/day08-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("14", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day08(File.ReadAllText("inputs/day08-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("332", answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day08(File.ReadAllText("inputs/day08-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("34", answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day08(File.ReadAllText("inputs/day08-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("1174", answer);
        }
    }
}