using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aoc2023;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// https://adventofcode.com/2023/day/1

namespace Aoc2023.Tests
{
    [TestClass()]
    public class Day01Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day01(File.ReadAllText("day01-example1.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(142, answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day01(File.ReadAllText("day01-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(53334, answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day01(File.ReadAllText("day01-example2.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(281, answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day01(File.ReadAllText("day01-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(52834, answer);
        }
        [TestMethod()]
        public void Part2RedditTest()
        {
            // https://www.reddit.com/r/adventofcode/comments/1884fpl/2023_day_1for_those_who_stuck_on_part_2/
            (string, int)[] tests =
            [
                ("eighthree", 83),
                ("sevenine", 79)
            ];
            foreach (var kvp in tests)
            {
                var instance = new Day01(kvp.Item1);
                var answer = instance.Part2();
                Assert.AreEqual(kvp.Item2, answer, $"Expected {kvp.Item1} to be {kvp.Item2}");
            }
        }
    }
}