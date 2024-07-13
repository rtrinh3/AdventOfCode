namespace Aoc2020.Tests
{
    // https://adventofcode.com/2020/day/21
    // --- Day 21: Allergen Assessment ---
    [TestClass()]
    public class Day21Tests
    {
        [TestMethod()]
        public void Day21_Part1_ExampleTest()
        {
            var instance = new Day21(File.ReadAllText("day21-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(5, answer);
        }
        [TestMethod()]
        public void Day21_Part1_InputTest()
        {
            var instance = new Day21(File.ReadAllText("day21-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(2162, answer);
        }

        [TestMethod()]
        public void Day21_Part2_ExampleTest()
        {
            var instance = new Day21(File.ReadAllText("day21-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("mxmxvkd,sqjhc,fvjkl".GetHashCode(), answer); // TODO Change Aoc2020.IAocDay to use strings
        }
        [TestMethod()]
        public void Day21_Part2_InputTest()
        {
            var instance = new Day21(File.ReadAllText("day21-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("lmzg,cxk,bsqh,bdvmx,cpbzbx,drbm,cfnt,kqprv".GetHashCode(), answer); // TODO Change Aoc2020.IAocDay to use strings
        }
    }
}