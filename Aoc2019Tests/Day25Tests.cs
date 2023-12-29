using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2019.Tests
{
    [TestClass()]
    public class Day25Tests
    {
        [TestMethod(), Timeout(60_000)]
        public void Test()
        {
            var instance = new Day25(File.ReadAllText("day25-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("1109393410", answer);
        }
    }
}