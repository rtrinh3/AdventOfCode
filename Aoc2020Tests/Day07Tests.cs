namespace Aoc2020.Tests
{
    [TestClass()]
    public class Day07Tests
    {
        [TestMethod()]
        public void Day07_Part1_ExampleTest()
        {
            var instance = new Day07(File.ReadAllText("day07-example1.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(4, answer);
        }
        [TestMethod()]
        public void Day07_Part1_InputTest()
        {
            var instance = new Day07(File.ReadAllText("day07-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(224, answer);
        }

        [TestMethod()]
        public void Day07_Part2_Example1Test()
        {
            var instance = new Day07(File.ReadAllText("day07-example1.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(32, answer);
        }
        [TestMethod()]
        public void Day07_Part2_Example2Test()
        {
            var instance = new Day07(File.ReadAllText("day07-example2.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(126, answer);
        }
        [TestMethod()]
        public void Day07_Part2_InputTest()
        {
            var instance = new Day07(File.ReadAllText("day07-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(1488, answer);
        }
    }
}