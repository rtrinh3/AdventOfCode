using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aoc2023;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023.Tests
{
    // https://adventofcode.com/2023/day/14
    [TestClass()]
    public class Day14Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day14(File.ReadAllText("inputs/day14-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(136, answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day14(File.ReadAllText("inputs/day14-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(109638, answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day14(File.ReadAllText("inputs/day14-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(64, answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day14(File.ReadAllText("inputs/day14-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(102657, answer);
        }
    }
}