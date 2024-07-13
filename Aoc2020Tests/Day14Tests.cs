namespace Aoc2020.Tests
{
    [TestClass()]
    public class Day14Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day14(File.ReadAllText("day14-example1.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("165", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day14(File.ReadAllText("day14-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("16003257187056", answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day14(File.ReadAllText("day14-example2.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("208", answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day14(File.ReadAllText("day14-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("3219837697833", answer);
        }
    }
}