namespace Aoc2024.Tests
{
    // https://adventofcode.com/2024/day/5
    // --- Day 5: Print Queue ---
    [TestClass()]
    public class Day05Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day05(File.ReadAllText("day05-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("143", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day05(File.ReadAllText("day05-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("5329", answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day05(File.ReadAllText("day05-example.txt"));
            var answer = instance.Part2();
            //Assert.AreEqual("18", answer);
            Assert.Inconclusive(answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day05(File.ReadAllText("day05-input.txt"));
            var answer = instance.Part2();
            //Assert.AreEqual("18", answer);
            Assert.Inconclusive(answer);
        }
    }
}