using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2019.Tests
{
    [TestClass()]
    public class Day01Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instanceA = new Day01("12");
            var answerA = instanceA.Part1();
            Assert.AreEqual("2", answerA);

            var instanceB = new Day01("14");
            var answerB = instanceB.Part1();
            Assert.AreEqual("2", answerB);

            var instanceC = new Day01("1969");
            var answerC = instanceC.Part1();
            Assert.AreEqual("654", answerC);

            var instanceD = new Day01("100756");
            var answerD = instanceD.Part1();
            Assert.AreEqual("33583", answerD);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day01(File.ReadAllText("day01-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("3388015", answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instanceA = new Day01("14");
            var answerA = instanceA.Part2();
            Assert.AreEqual("2", answerA);

            var instanceB = new Day01("1969");
            var answerB = instanceB.Part2();
            Assert.AreEqual("966", answerB);

            var instanceC = new Day01("100756");
            var answerC = instanceC.Part2();
            Assert.AreEqual("50346", answerC);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day01(File.ReadAllText("day01-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("5079140", answer);
        }
    }
}