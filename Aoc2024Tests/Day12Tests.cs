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
            var instance = new Day12(File.ReadAllText("day12-example1.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("140", answer);
        }
        [TestMethod()]
        public void Part1Example2Test()
        {
            var instance = new Day12(File.ReadAllText("day12-example2.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("772", answer);
        }
        [TestMethod()]
        public void Part1Example3Test()
        {
            var instance = new Day12(File.ReadAllText("day12-example3.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("1930", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day12(File.ReadAllText("day12-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("1533644", answer);
        }

        //[TestMethod()]
        //public void Part2ExampleTest()
        //{
        //    var instance = new Day12(File.ReadAllText("day12-example.txt"));
        //    var answer = instance.Part2();
        //    Assert.AreEqual("81", answer);
        //}
        //[TestMethod()]
        //public void Part2InputTest()
        //{
        //    var instance = new Day12(File.ReadAllText("day12-input.txt"));
        //    var answer = instance.Part2();
        //    Assert.AreEqual("928", answer);
        //}
    }
}