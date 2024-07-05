namespace Aoc2020.Tests
{
    [TestClass()]
    public class Day15Tests
    {
        [TestMethod()]
        public void Day15_Part1_Example1Test()
        {
            var instance = new Day15(File.ReadAllText("day15-example1.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(436, answer);
        }
        [TestMethod()]
        public void Day15_Part1_Example2Test()
        {
            var instance = new Day15(File.ReadAllText("day15-example2.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(1, answer);
        }
        [TestMethod()]
        public void Day15_Part1_Example3Test()
        {
            var instance = new Day15(File.ReadAllText("day15-example3.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(10, answer);
        }
        [TestMethod()]
        public void Day15_Part1_Example4Test()
        {
            var instance = new Day15(File.ReadAllText("day15-example4.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(27, answer);
        }
        [TestMethod()]
        public void Day15_Part1_Example5Test()
        {
            var instance = new Day15(File.ReadAllText("day15-example5.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(78, answer);
        }
        [TestMethod()]
        public void Day15_Part1_Example6Test()
        {
            var instance = new Day15(File.ReadAllText("day15-example6.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(438, answer);
        }
        [TestMethod()]
        public void Day15_Part1_Example7Test()
        {
            var instance = new Day15(File.ReadAllText("day15-example7.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(1836, answer);
        }
        [TestMethod()]
        public void Day15_Part1_InputTest()
        {
            var instance = new Day15(File.ReadAllText("day15-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(240, answer);
        }

        [TestMethod(), Timeout(10_000)]
        public void Day15_Part2_Example1Test()
        {
            var instance = new Day15(File.ReadAllText("day15-example1.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(175594, answer);
        }
        [TestMethod(), Timeout(10_000)]
        public void Day15_Part2_Example2Test()
        {
            var instance = new Day15(File.ReadAllText("day15-example2.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(2578, answer);
        }
        [TestMethod(), Timeout(10_000)]
        public void Day15_Part2_Example3Test()
        {
            var instance = new Day15(File.ReadAllText("day15-example3.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(3544142, answer);
        }
        [TestMethod(), Timeout(10_000)]
        public void Day15_Part2_Example4Test()
        {
            var instance = new Day15(File.ReadAllText("day15-example4.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(261214, answer);
        }
        [TestMethod(), Timeout(10_000)]
        public void Day15_Part2_Example5Test()
        {
            var instance = new Day15(File.ReadAllText("day15-example5.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(6895259, answer);
        }
        [TestMethod(), Timeout(10_000)]
        public void Day15_Part2_Example6Test()
        {
            var instance = new Day15(File.ReadAllText("day15-example6.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(18, answer);
        }
        [TestMethod(), Timeout(10_000)]
        public void Day15_Part2_Example7Test()
        {
            var instance = new Day15(File.ReadAllText("day15-example7.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(362, answer);
        }
        [TestMethod(), Timeout(10_000)]
        public void Day15_Part2_InputTest()
        {
            var instance = new Day15(File.ReadAllText("day15-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(505, answer);
        }
    }
}