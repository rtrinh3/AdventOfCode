using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2022.Tests
{
    [TestClass()]
    public class Day10Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day10(File.ReadAllText("inputs/day10-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("13140", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day10(File.ReadAllText("inputs/day10-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("15120", answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day10(File.ReadAllText("inputs/day10-example.txt"));
            var answer = instance.Part2();
            var counts = answer.GroupBy(c => c).OrderByDescending(g => g.Count()).ToList();
            Assert.IsTrue(counts.Count >= 3);
            Assert.AreEqual(124, counts[0].Count());
            Assert.AreEqual(116, counts[1].Count());
            Assert.AreEqual(6, counts[2].Count());
            string normalized = answer.ReplaceLineEndings("\n").Replace(counts[0].Key, '#').Replace(counts[1].Key, '.');
            string expected = @"##..##..##..##..##..##..##..##..##..##..
###...###...###...###...###...###...###.
####....####....####....####....####....
#####.....#####.....#####.....#####.....
######......######......######......####
#######.......#######.......#######.....
".ReplaceLineEndings("\n");
            Assert.AreEqual(expected, normalized);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day10(File.ReadAllText("inputs/day10-input.txt"));
            var answer = instance.Part2();
            var counts = answer.GroupBy(c => c).OrderByDescending(g => g.Count()).ToList();
            Assert.IsTrue(counts.Count >= 3);
            Assert.AreEqual(143, counts[0].Count());
            Assert.AreEqual(97, counts[1].Count());
            Assert.AreEqual(6, counts[2].Count());
            string normalized = answer.ReplaceLineEndings("\n").Replace(counts[0].Key, '.').Replace(counts[1].Key, '#');
            string expected = @"###..#..#.###....##.###..###..#.....##..
#..#.#.#..#..#....#.#..#.#..#.#....#..#.
#..#.##...#..#....#.###..#..#.#....#..#.
###..#.#..###.....#.#..#.###..#....####.
#.#..#.#..#....#..#.#..#.#....#....#..#.
#..#.#..#.#.....##..###..#....####.#..#.
".ReplaceLineEndings("\n");
            Assert.AreEqual(expected, normalized);
        }
    }
}