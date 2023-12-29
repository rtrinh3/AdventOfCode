using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2019.Tests
{
    [TestClass()]
    public class Day11Tests
    {
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day11(File.ReadAllText("day11-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("2883", answer);
        }

        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day11(File.ReadAllText("day11-input.txt"));
            var answer = instance.Part2().ReplaceLineEndings("\n");

            string expected = @".#....####.###...##..###..#.....##..####...
.#....#....#..#.#..#.#..#.#....#..#....#...
.#....###..#..#.#....#..#.#....#......#....
.#....#....###..#....###..#....#.##..#.....
.#....#....#....#..#.#....#....#..#.#......
.####.####.#.....##..#....####..###.####...
".ReplaceLineEndings("\n");
            var answerStats = answer.GroupBy(c => c).Select(g => (g.Key, g.Count())).OrderByDescending(g => g.Item2).ToList();
            var expectedStats = expected.GroupBy(c => c).Select(g => (g.Key, g.Count())).OrderByDescending(g => g.Item2).ToList();
            Assert.IsTrue(expectedStats.Select(g => g.Item2).SequenceEqual(answerStats.Select(g => g.Item2)));
            string normalizedAnswer = answer.Replace(answerStats[0].Key, expectedStats[0].Key).Replace(answerStats[1].Key, expectedStats[1].Key);
            Assert.AreEqual(expected, normalizedAnswer);
        }
    }
}