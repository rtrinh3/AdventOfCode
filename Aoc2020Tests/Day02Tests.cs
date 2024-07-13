namespace Aoc2020.Tests
{
    [TestClass()]
    public class Day02Tests
    {
        [TestMethod()]
        public void Day02_Part1_ExampleTest()
        {
            var instance = new Day02(File.ReadAllText("day02-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("2", answer);
        }
        [TestMethod()]
        public void Day02_Part1_InputTest()
        {
            var instance = new Day02(File.ReadAllText("day02-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("506", answer);
        }

        [TestMethod()]
        public void Day02_Part2_ExampleTest()
        {
            var instance = new Day02(File.ReadAllText("day02-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("1", answer);
        }
        [TestMethod()]
        public void Day02_Part2_InputTest()
        {
            var instance = new Day02(File.ReadAllText("day02-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("443", answer);
        }
    }
}