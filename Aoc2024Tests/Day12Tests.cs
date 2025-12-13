namespace Aoc2024.Tests
{
    // https://adventofcode.com/2024/day/12
    // --- Day 12: Garden Groups ---
    [TestClass()]
    public class Day12Tests
    {
        [TestMethod()]
        public void Part1Example1Test()
        {
            var instance = new Day12(File.ReadAllText("inputs/day12-example1.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("140", answer);
        }
        [TestMethod()]
        public void Part1Example2Test()
        {
            var instance = new Day12(File.ReadAllText("inputs/day12-example2.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("772", answer);
        }
        [TestMethod()]
        public void Part1Example3Test()
        {
            var instance = new Day12(File.ReadAllText("inputs/day12-example3.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("1930", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day12(File.ReadAllText("inputs/day12-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("1533644", answer);
        }

        [TestMethod()]
        public void Part2Example1Test()
        {
            var instance = new Day12(File.ReadAllText("inputs/day12-example1.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("80", answer);
        }
        [TestMethod()]
        public void Part2Example2Test()
        {
            var instance = new Day12(File.ReadAllText("inputs/day12-example2.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("436", answer);
        }
        [TestMethod()]
        public void Part2Example3Test()
        {
            var instance = new Day12(File.ReadAllText("inputs/day12-example3.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("1206", answer);
        }
        [TestMethod()]
        public void Part2Example4Test()
        {
            var instance = new Day12(File.ReadAllText("inputs/day12-example4.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("236", answer);
        }
        [TestMethod()]
        public void Part2Example5Test()
        {
            var instance = new Day12(File.ReadAllText("inputs/day12-example5.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("368", answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day12(File.ReadAllText("inputs/day12-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("936718", answer);
        }
    }
}