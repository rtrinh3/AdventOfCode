namespace Aoc2020.Tests
{
    // https://adventofcode.com/2020/day/21
    // --- Day 21: Allergen Assessment ---
    [TestClass()]
    public class Day21Tests
    {
        [TestMethod()]
        public void Day21_Part1_ExampleTest()
        {
            var instance = new Day21(File.ReadAllText("day21-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(5, answer);
        }
        [TestMethod()]
        public void Day21_Part1_InputTest()
        {
            var instance = new Day21(File.ReadAllText("day21-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(2162, answer);
        }

        [TestMethod()]
        public void Day21_Part2_ExampleTest()
        {
            var instance = new Day21(File.ReadAllText("day21-example.txt"));
            var answer = instance.Part2();
            //Assert.AreEqual(54755174472007, answer);
            Assert.Inconclusive(answer.ToString());
        }
        [TestMethod()]
        public void Day21_Part2_InputTest()
        {
            var instance = new Day21(File.ReadAllText("day21-input.txt"));
            var answer = instance.Part2();
            //Assert.AreEqual(54755174472007, answer);
            Assert.Inconclusive(answer.ToString());
        }
    }
}