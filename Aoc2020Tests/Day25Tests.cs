namespace Aoc2020.Tests
{
    // https://adventofcode.com/2020/day/25
    // --- Day 25: Combo Breaker ---
    [TestClass()]
    public class Day25Tests
    {
        [TestMethod()]
        public void Day25_Example_Test()
        {
            var instance = new Day25(File.ReadAllText("day25-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("14897079", answer);
        }

        [TestMethod()]
        public void Day25_Input_Test()
        {
            var instance = new Day25(File.ReadAllText("day25-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("16933668", answer);
        }
    }
}