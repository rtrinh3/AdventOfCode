namespace Aoc2020.Tests
{
    // https://adventofcode.com/2020/day/13
    // --- Day 13: Shuttle Search ---
    [TestClass()]
    public class Day13Tests
    {
        [TestMethod()]
        public void Day13_Part1_ExampleTest()
        {
            var instance = new Day13(File.ReadAllText("day13-example1.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(295, answer);
        }
        [TestMethod()]
        public void Day13_Part1_InputTest()
        {
            var instance = new Day13(File.ReadAllText("day13-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(3789, answer);
        }

        [TestMethod()]
        public void Day13_Part2_Example1Test()
        {
            var instance = new Day13(File.ReadAllText("day13-example1.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(1068781, answer);
        }
        [TestMethod()]
        public void Day13_Part2_Example2Test()
        {
            var instance = new Day13(File.ReadAllText("day13-example2.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(3417, answer);
        }
        [TestMethod()]
        public void Day13_Part2_Example3Test()
        {
            var instance = new Day13(File.ReadAllText("day13-example3.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(754018, answer);
        }
        [TestMethod()]
        public void Day13_Part2_Example4Test()
        {
            var instance = new Day13(File.ReadAllText("day13-example4.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(779210, answer);
        }
        [TestMethod()]
        public void Day13_Part2_Example5Test()
        {
            var instance = new Day13(File.ReadAllText("day13-example5.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(1261476, answer);
        }
        [TestMethod()]
        public void Day13_Part2_Example6Test()
        {
            var instance = new Day13(File.ReadAllText("day13-example6.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(1202161486, answer);
        }
        [TestMethod()]
        public void Day13_Part2_InputTest()
        {
            var instance = new Day13(File.ReadAllText("day13-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(667437230788118, answer);
        }
    }
}