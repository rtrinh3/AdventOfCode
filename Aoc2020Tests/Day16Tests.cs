namespace Aoc2020.Tests
{
    [TestClass()]
    public class Day16Tests
    {
        [TestMethod()]
        public void Day16_Part1_Example1Test()
        {
            var instance = new Day16(File.ReadAllText("day16-example1.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(71, answer);
        }
        [TestMethod()]
        public void Day16_Part1_InputTest()
        {
            var instance = new Day16(File.ReadAllText("day16-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(23122, answer);
        }

        [TestMethod()]
        public void Day16_Part2_Example2Test()
        {
            var instance = new Day16(File.ReadAllText("day16-example2.txt"));
            var answer = instance.FindFieldOrder();
            Assert.IsTrue(answer.SequenceEqual(["row", "class", "seat"]));
        }
        [TestMethod()]
        public void Day16_Part2_InputTest()
        {
            var instance = new Day16(File.ReadAllText("day16-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(362974212989, answer);
        }
    }
}