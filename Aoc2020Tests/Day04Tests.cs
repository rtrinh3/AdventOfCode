namespace Aoc2020.Tests
{
    [TestClass()]
    public class Day04Tests
    {
        [TestMethod()]
        public void Day04_Part1_Example1Test()
        {
            var instance = new Day04(File.ReadAllText("day04-example1.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(2, answer);
        }
        [TestMethod()]
        public void Day04_Part1_InputTest()
        {
            var instance = new Day04(File.ReadAllText("day04-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(260, answer);
        }

        [TestMethod()]
        public void Day04_Part2_Example2Test()
        {
            var instance = new Day04(File.ReadAllText("day04-example2.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(0, answer);
        }
        [TestMethod()]
        public void Day04_Part2_Example3Test()
        {
            var instance = new Day04(File.ReadAllText("day04-example3.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(4, answer);
        }
        [TestMethod()]
        public void Day04_Part2_InputTest()
        {
            var instance = new Day04(File.ReadAllText("day04-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(153, answer);
        }
    }
}