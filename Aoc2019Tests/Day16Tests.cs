using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2019.Tests
{
    [TestClass()]
    public class Day16Tests
    {
        [TestMethod()]
        public void Part1Example1Test()
        {
            var instance = new Day16(File.ReadAllText("day16-example1.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("24176176", answer);
        }
        [TestMethod()]
        public void Part1Example2Test()
        {
            var instance = new Day16(File.ReadAllText("day16-example2.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("73745418", answer);
        }
        [TestMethod()]
        public void Part1Example3Test()
        {
            var instance = new Day16(File.ReadAllText("day16-example3.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("52432133", answer);
        }
        [TestMethod(), Timeout(5000)]
        public void Part1InputTest()
        {
            var instance = new Day16(File.ReadAllText("day16-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("88323090", answer);
        }

        [TestMethod()]
        public void Part2Example4Test()
        {
            var instance = new Day16(File.ReadAllText("day16-example4.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("84462026", answer);
        }
        [TestMethod()]
        public void Part2Example5Test()
        {
            var instance = new Day16(File.ReadAllText("day16-example5.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("78725270", answer);
        }
        [TestMethod()]
        public void Part2Example6Test()
        {
            var instance = new Day16(File.ReadAllText("day16-example6.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("53553731", answer);
        }
        [TestMethod(), Timeout(5000)]
        public void Part2InputTest()
        {
            var instance = new Day16(File.ReadAllText("day16-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("50077964", answer);
        }
    }
}