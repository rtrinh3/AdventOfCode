namespace Aoc2020.Tests
{
    [TestClass()]
    public class Day03Tests
    {
        [TestMethod()]
        public void Day03_Part1_ExampleTest()
        {
            var instance = new Day03(File.ReadAllText("inputs/day03-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("7", answer);
        }
        [TestMethod()]
        public void Day03_Part1_InputTest()
        {
            var instance = new Day03(File.ReadAllText("inputs/day03-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("153", answer);
        }

        [TestMethod()]
        public void Day03_Part2_ExampleTest()
        {
            var instance = new Day03(File.ReadAllText("inputs/day03-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("336", answer);
        }
        [TestMethod()]
        public void Day03_Part2_InputTest()
        {
            var instance = new Day03(File.ReadAllText("inputs/day03-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("2421944712", answer);
        }
    }
}