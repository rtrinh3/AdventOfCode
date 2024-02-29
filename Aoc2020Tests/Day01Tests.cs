namespace Aoc2020.Tests
{
    [TestClass()]
    public class Day01Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day01(File.ReadAllText("day01-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(514579, answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day01(File.ReadAllText("day01-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(299299, answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day01(File.ReadAllText("day01-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(241861950, answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day01(File.ReadAllText("day01-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(287730716, answer);
        }
    }
}