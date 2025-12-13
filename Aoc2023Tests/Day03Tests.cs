using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aoc2023;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023.Tests
{
    // https://adventofcode.com/2023/day/3
    [TestClass()]
    public class Day03Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day03(File.ReadAllText("inputs/day03-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(4361, answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day03(File.ReadAllText("inputs/day03-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(539637, answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day03(File.ReadAllText("inputs/day03-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(467835, answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day03(File.ReadAllText("inputs/day03-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(82818007, answer);
        }
    }
}