using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2022.Tests
{
    [TestClass()]
    public class Day21Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day21(File.ReadAllText("inputs/day21-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("152", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day21(File.ReadAllText("inputs/day21-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("169525884255464", answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day21(File.ReadAllText("inputs/day21-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("301", answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day21(File.ReadAllText("inputs/day21-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("3247317268284", answer);
        }
    }
}