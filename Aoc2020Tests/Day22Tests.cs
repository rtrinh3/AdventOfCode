namespace Aoc2020.Tests
{
    // https://adventofcode.com/2020/day/22
    // --- Day 22: Crab Combat ---
    [TestClass()]
    public class Day22Tests
    {
        [TestMethod()]
        public void Day22_Part1_ExampleTest()
        {
            var instance = new Day22(File.ReadAllText("day22-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("306", answer);
        }
        [TestMethod()]
        public void Day22_Part1_InputTest()
        {
            var instance = new Day22(File.ReadAllText("day22-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("31781", answer);
        }

        [TestMethod()]
        public void Day22_Part2_ExampleTest()
        {
            var instance = new Day22(File.ReadAllText("day22-example.txt"));
            var answer = instance.Part2();
            //Assert.AreEqual("54755174472007", answer);
            Assert.Inconclusive(answer.ToString());
        }
        [TestMethod()]
        public void Day22_Part2_InputTest()
        {
            var instance = new Day22(File.ReadAllText("day22-input.txt"));
            var answer = instance.Part2();
            //Assert.AreEqual("54755174472007", answer);
            Assert.Inconclusive(answer.ToString());
        }
    }
}