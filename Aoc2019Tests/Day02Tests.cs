using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;
using System.Text.Json;

namespace Aoc2019.Tests
{
    [TestClass()]
    public class Day02Tests
    {
        private record ExampleProgram(string Input, string Expected);

        [TestMethod()]
        public void Part1ExamplesTest()
        {
            string examplesText = File.ReadAllText("inputs/day02-examples.txt");
            var examples = JsonSerializer.Deserialize<ExampleProgram[]>(examplesText);
            foreach (var example in examples)
            {
                var interpreter = new IntcodeInterpreter(example.Input);
                var expected = example.Expected.Split(',').Select(BigInteger.Parse).ToArray();
                
                _ = interpreter.RunToEnd().ToList();
                var memory = Enumerable.Range(0, expected.Length).Select(i => interpreter.Peek(i)).ToArray();

                Assert.IsTrue(memory.SequenceEqual(expected));
            }
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day02(File.ReadAllText("inputs/day02-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("3716293", answer);
        }

        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day02(File.ReadAllText("inputs/day02-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("6429", answer);
        }
    }
}