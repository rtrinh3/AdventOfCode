namespace Aoc2020.Tests
{
    // https://adventofcode.com/2020/day/20
    // --- Day 20: Jurassic Jigsaw ---
    [TestClass()]
    public class Day20Tests
    {
        [TestMethod()]
        public void Day20_Part1_ExampleTest()
        {
            var instance = new Day20(File.ReadAllText("day20-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("20899048083289", answer);
        }
        [TestMethod()]
        public void Day20_Part1_InputTest()
        {
            var instance = new Day20(File.ReadAllText("day20-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("54755174472007", answer);
        }

        [TestMethod()]
        public void Day20_Part2_ExampleTest()
        {
            var instance = new Day20(File.ReadAllText("day20-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("273", answer);
        }
        [TestMethod()]
        public void Day20_Part2_InputTest()
        {
            var instance = new Day20(File.ReadAllText("day20-input.txt"));
            var answer = instance.Part2();
            //Assert.AreEqual("107", answer);
            Assert.Inconclusive(answer.ToString());
        }
    }
}