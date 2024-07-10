namespace Aoc2020.Tests
{
    // https://adventofcode.com/2020/day/19
    // --- Day 19: Monster Messages ---
    [TestClass()]
    public class Day19Tests
    {
        [TestMethod()]
        public void Day19_Part1_Example1Test()
        {
            var instance = new Day19(File.ReadAllText("day19-example1.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(2, answer);
        }
        [TestMethod()]
        public void Day19_Part1_Example2Test()
        {
            var instance = new Day19(File.ReadAllText("day19-example2.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(3, answer);
        }
        [TestMethod()]
        public void Day19_Part1_InputTest()
        {
            var instance = new Day19(File.ReadAllText("day19-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(107, answer);
        }

        [TestMethod()]
        public void Day19_Part2_ExampleTest()
        {
            var instance = new Day19(File.ReadAllText("day19-example2.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(12, answer);
        }
        [TestMethod()]
        public void Day19_Part2_InputTest()
        {
            var instance = new Day19(File.ReadAllText("day19-input.txt"));
            var answer = instance.Part2();
            //Assert.AreEqual(1319, answer);
            Assert.Inconclusive(answer.ToString());
        }
    }
}