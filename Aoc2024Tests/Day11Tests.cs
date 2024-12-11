namespace Aoc2024.Tests
{
    // https://adventofcode.com/2024/day/11
    // --- Day 11: Plutonian Pebbles ---
    [TestClass()]
    public class Day11Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day11(File.ReadAllText("day11-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("55312", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day11(File.ReadAllText("day11-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("198089", answer);
        }

        //[TestMethod()]
        //public void Part2ExampleTest()
        //{
        //    var instance = new Day11(File.ReadAllText("day11-example.txt"));
        //    var answer = instance.Part2();
        //    //Assert.AreEqual("430", answer);
        //    Assert.Inconclusive(answer);
        //}
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day11(File.ReadAllText("day11-input.txt"));
            var answer = instance.Part2();
            //Assert.AreEqual("430", answer);
            Assert.Inconclusive(answer);
        }
    }
}