namespace Aoc2020.Tests
{
    [TestClass()]
    public class Day06Tests
    {
        [TestMethod()]
        public void Day06_Part1_ExampleTest()
        {
            var instance = new Day06(File.ReadAllText("day06-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(11, answer);
        }
        [TestMethod()]
        public void Day06_Part1_InputTest()
        {
            var instance = new Day06(File.ReadAllText("day06-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(6521, answer);
        }

        [TestMethod()]
        public void Day06_Part2_ExampleTest()
        {
            var instance = new Day06(File.ReadAllText("day06-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(6, answer);
        }
        [TestMethod()]
        public void Day06_Part2_InputTest()
        {
            var instance = new Day06(File.ReadAllText("day06-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(3305, answer);
        }
    }
}