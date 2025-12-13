namespace Aoc2020.Tests
{
    [TestClass()]
    public class Day05Tests
    {
        [TestMethod()]
        public void Day05_Part1_ExampleTest()
        {
            (string input, int expected)[] examples = [ 
                ("FBFBBFFRLR", 357),
                ("BFFFBBFRRR", 567),
                ("FFFBBBFRRR", 119),
                ("BBFFBBFRLL", 820),
            ];
            foreach (var (input, expected) in examples)
            {
                var instance = new Day05(input);
                var answer = instance.Part1();
                Assert.AreEqual(expected.ToString(), answer);
            }
        }
        [TestMethod()]
        public void Day05_Part1_InputTest()
        {
            var instance = new Day05(File.ReadAllText("inputs/day05-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("842", answer);
        }

        [TestMethod()]
        public void Day05_Part2_InputTest()
        {
            var instance = new Day05(File.ReadAllText("inputs/day05-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("617", answer);
        }
    }
}