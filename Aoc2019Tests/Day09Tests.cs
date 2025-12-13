using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace Aoc2019.Tests
{
    // https://adventofcode.com/2019/day/9
    [TestClass()]
    public class Day09Tests
    {
        [TestMethod()]
        public void Part1Example1Test()
        {
            var program = File.ReadAllText("inputs/day09-example1.txt");
            var numbers = program.Split(',').Select(BigInteger.Parse).ToList();
            var interpreter = new IntcodeInterpreter(numbers);
            var output = interpreter.RunToEnd().ToList();
            Assert.IsTrue(numbers.SequenceEqual(output));
        }
        [TestMethod()]
        public void Part1Example2Test()
        {
            var program = File.ReadAllText("inputs/day09-example2.txt");
            var interpreter = new IntcodeInterpreter(program);
            var output = interpreter.RunToEnd().Single();
            Assert.AreEqual(16, output.ToString().Length);
        }
        [TestMethod()]
        public void Part1Example3Test()
        {
            var program = File.ReadAllText("inputs/day09-example3.txt");
            var numbers = program.Split(',', AocCommon.Parsing.TrimAndDiscard);
            var interpreter = new IntcodeInterpreter(program);
            var output = interpreter.RunToEnd().Single();
            Assert.AreEqual(numbers[1], output.ToString());
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day09(File.ReadAllText("inputs/day09-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("3989758265", answer);
        }

        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day09(File.ReadAllText("inputs/day09-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("76791", answer);
        }
    }
}