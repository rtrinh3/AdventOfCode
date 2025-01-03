namespace Aoc2024.Tests
{
    // https://adventofcode.com/2024/day/11
    // --- Day 11: Plutonian Pebbles ---
    [TestClass()]
    public class Day11Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day11(File.ReadAllText("day11-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("55312", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day11(File.ReadAllText("day11-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("198089", answer);
        }

        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day11(File.ReadAllText("day11-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("236302670835517", answer);
        }

        [TestMethod()]
        public void Part3ExampleTest()
        {
            // https://www.reddit.com/r/adventofcode/comments/1hqoc5w/2024_day_11_i_made_a_part_3_to_this_day/
            // https://breakmessage.com/aocextension/2024day11/
            var instance = new Day11(File.ReadAllText("day11-example.txt"));
            (int iterations, string expected)[] tests =
            {
                (4, "72485"),
                (6, "44893"),
                (25, "2507617947379703"),
                (75, "4490957455945801063263206957330247")
            };
            foreach (var test in tests)
            {
                var answer = instance.Part3(test.iterations);
                Assert.AreEqual(test.expected, answer);
            }
        }
    }
}