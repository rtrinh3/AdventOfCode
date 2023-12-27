using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2022.Tests
{
    [TestClass()]
    public class Day07Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day07(File.ReadAllText("day07-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("95437", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day07(File.ReadAllText("day07-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("1206825", answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day07(File.ReadAllText("day07-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("24933642", answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day07(File.ReadAllText("day07-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("9608311", answer);
        }
    }
}