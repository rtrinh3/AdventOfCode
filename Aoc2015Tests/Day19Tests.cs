namespace Aoc2015.Tests
{
    [TestClass()]
    public class Day19Tests
    {
        [TestMethod()]
        public void Part1Example1Test()
        {
            var instance = new Day19(File.ReadAllText("day19-example1.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("4", answer);
        }
        [TestMethod()]
        public void Part1Example2Test()
        {
            var instance = new Day19(File.ReadAllText("day19-example2.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("7", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day19(File.ReadAllText("day19-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("518", answer);
        }

        //[TestMethod()]
        //public void Part2_Test()
        //{
        //    Assert.Fail();
        //}
    }
}
