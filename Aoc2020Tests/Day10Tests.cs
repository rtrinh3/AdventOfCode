namespace Aoc2020.Tests
{
    // https://adventofcode.com/2020/day/10
    // --- Day 10: Adapter Array ---
    [TestClass()]
    public class Day10Tests
    {
        [TestMethod()]
        public void Day10_Part1_Example1Test()
        {
            var instance = new Day10(File.ReadAllText("inputs/day10-example1.txt"));
            var answer = instance.DoPart1();
            Assert.AreEqual((7, 5), answer);
        }
        [TestMethod()]
        public void Day10_Part1_Example2Test()
        {
            var instance = new Day10(File.ReadAllText("inputs/day10-example2.txt"));
            var answer = instance.DoPart1();
            Assert.AreEqual((22, 10), answer);
        }
        [TestMethod()]
        public void Day10_Part1_InputTest()
        {
            var instance = new Day10(File.ReadAllText("inputs/day10-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("2380", answer);
        }

        [TestMethod()]
        public void Day10_Part2_Example1Test()
        {
            var instance = new Day10(File.ReadAllText("inputs/day10-example1.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("8", answer);
        }
        [TestMethod()]
        public void Day10_Part2_Example2Test()
        {
            var instance = new Day10(File.ReadAllText("inputs/day10-example2.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("19208", answer);
        }
        [TestMethod()]
        public void Day10_Part2_InputTest()
        {
            var instance = new Day10(File.ReadAllText("inputs/day10-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("48358655787008", answer);
        }
    }
}