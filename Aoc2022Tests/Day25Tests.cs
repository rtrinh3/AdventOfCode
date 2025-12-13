using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2022.Tests
{
    [TestClass()]
    public class Day25Tests
    {
        [TestMethod()]
        public void ExampleTest()
        {
            var instance = new Day25(File.ReadAllText("inputs/day25-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("2=-1=0", answer);
        }
        [TestMethod()]
        public void InputTest()
        {
            var instance = new Day25(File.ReadAllText("inputs/day25-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("2-2--02=1---1200=0-1", answer);
        }
    }
}