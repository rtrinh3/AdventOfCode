using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2019.Tests
{
    [TestClass()]
    public class Day08Tests
    {
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day08(File.ReadAllText("day08-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("1330", answer);
        }

        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day08(File.ReadAllText("day08-input.txt"));
            var answer = instance.Part2();
            const string expected = @"1111001100100101111011110
1000010010100101000010000
1110010010111101110011100
1000011110100101000010000
1000010010100101000010000
1000010010100101111010000
";
            Assert.AreEqual(expected.ReplaceLineEndings("\n"), answer.ReplaceLineEndings("\n"));
        }
    }
}