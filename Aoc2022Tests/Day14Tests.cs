using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2022.Tests
{
    [TestClass()]
    public class Day14Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day14(File.ReadAllText("inputs/day14-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("24", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day14(File.ReadAllText("inputs/day14-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("1199", answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day14(File.ReadAllText("inputs/day14-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("93", answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day14(File.ReadAllText("inputs/day14-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("23925", answer);
        }
    }
}