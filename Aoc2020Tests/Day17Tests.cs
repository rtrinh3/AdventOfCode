namespace Aoc2020.Tests
{
    // https://adventofcode.com/2020/day/17
    // --- Day 17: Conway Cubes ---
    [TestClass()]
    public class Day17Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day17(File.ReadAllText("inputs/day17-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("112", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day17(File.ReadAllText("inputs/day17-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("232", answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day17(File.ReadAllText("inputs/day17-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("848", answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day17(File.ReadAllText("inputs/day17-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("1620", answer);
        }
    }
}