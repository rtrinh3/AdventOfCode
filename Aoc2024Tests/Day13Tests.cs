namespace Aoc2024.Tests
{
    // https://adventofcode.com/2024/day/13
    // --- Day 13: Claw Contraption ---
    [TestClass()]
    public class Day13Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day13(File.ReadAllText("day13-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("480", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day13(File.ReadAllText("day13-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("37128", answer);
        }

        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day13(File.ReadAllText("day13-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("74914228471331", answer);
        }
    }
}