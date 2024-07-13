namespace Aoc2020.Tests
{
    // https://adventofcode.com/2020/day/18
    // --- Day 18: Operation Order ---
    [TestClass()]
    public class Day18Tests
    {
        [TestMethod()]
        public void Part1_Example1_Test()
        {
            var instance = new Day18(File.ReadAllText("day18-example1.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("71", answer);
        }
        [TestMethod()]
        public void Part1_Example2_Test()
        {
            var instance = new Day18(File.ReadAllText("day18-example2.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("51", answer);
        }
        [TestMethod()]
        public void Part1_Example3_Test()
        {
            var instance = new Day18(File.ReadAllText("day18-example3.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("26", answer);
        }
        [TestMethod()]
        public void Part1_Example4_Test()
        {
            var instance = new Day18(File.ReadAllText("day18-example4.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("437", answer);
        }
        [TestMethod()]
        public void Part1_Example5_Test()
        {
            var instance = new Day18(File.ReadAllText("day18-example5.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("12240", answer);
        }
        [TestMethod()]
        public void Part1_Example6_Test()
        {
            var instance = new Day18(File.ReadAllText("day18-example6.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("13632", answer);
        }
        [TestMethod()]
        public void Part1_Input_Test()
        {
            var instance = new Day18(File.ReadAllText("day18-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("21993583522852", answer);
        }

        [TestMethod()]
        public void Part2_Example1_Test()
        {
            var instance = new Day18(File.ReadAllText("day18-example1.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("231", answer);
        }
        [TestMethod()]
        public void Part2_Example2_Test()
        {
            var instance = new Day18(File.ReadAllText("day18-example2.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("51", answer);
        }
        [TestMethod()]
        public void Part2_Example3_Test()
        {
            var instance = new Day18(File.ReadAllText("day18-example3.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("46", answer);
        }
        [TestMethod()]
        public void Part2_Example4_Test()
        {
            var instance = new Day18(File.ReadAllText("day18-example4.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("1445", answer);
        }
        [TestMethod()]
        public void Part2_Example5_Test()
        {
            var instance = new Day18(File.ReadAllText("day18-example5.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("669060", answer);
        }
        [TestMethod()]
        public void Part2_Example6_Test()
        {
            var instance = new Day18(File.ReadAllText("day18-example6.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("23340", answer);
        }
        [TestMethod()]
        public void Part2_Input_Test()
        {
            var instance = new Day18(File.ReadAllText("day18-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("122438593522757", answer);
        }
    }
}
