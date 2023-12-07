using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aoc2023;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023.Tests
{
    [TestClass()]
    public class Day07Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day07(File.ReadAllText("day07-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(6440, answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day07(File.ReadAllText("day07-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(252295678, answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day07(File.ReadAllText("day07-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(5905, answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day07(File.ReadAllText("day07-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(250577259, answer);
        }

        [TestMethod()]
        public void RedditTest()
        {
            // https://www.reddit.com/r/adventofcode/comments/18cr4xr/2023_day_7_better_example_input_not_a_spoiler/
            var instance = new Day07(@"2345A 1
Q2KJJ 13
Q2Q2Q 19
T3T3J 17
T3Q33 11
2345J 3
J345A 2
32T3K 5
T55J5 29
KK677 7
KTJJT 34
QQQJA 31
JJJJJ 37
JAAAA 43
AAAAJ 59
AAAAA 61
2AAAA 23
2JJJJ 53
JJJJ2 41");
            Assert.AreEqual(6592, instance.Part1());
            Assert.AreEqual(6839, instance.Part2());
        }
    }
}