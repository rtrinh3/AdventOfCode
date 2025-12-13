using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aoc2023;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023.Tests
{
    // https://adventofcode.com/2023/day/15
    [TestClass()]
    public class Day15Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day15(File.ReadAllText("inputs/day15-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(1320, answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day15(File.ReadAllText("inputs/day15-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(513214, answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day15(File.ReadAllText("inputs/day15-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(145, answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day15(File.ReadAllText("inputs/day15-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(258826, answer);
        }
    }
}