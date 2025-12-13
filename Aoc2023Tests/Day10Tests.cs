using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aoc2023;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023.Tests
{
    // https://adventofcode.com/2023/day/10
    [TestClass()]
    public class Day10Tests
    {
        [TestMethod()]
        public void Part1Example1Test()
        {
            var instance = new Day10(File.ReadAllText("inputs/day10-example1.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(4, answer);
        }
        [TestMethod()]
        public void Part1Example2Test()
        {
            var instance = new Day10(File.ReadAllText("inputs/day10-example2.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(8, answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day10(File.ReadAllText("inputs/day10-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(6806, answer);
        }

        [TestMethod()]
        public void Part2Example3Test()
        {
            var instance = new Day10(File.ReadAllText("inputs/day10-example3.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(4, answer);
        }
        [TestMethod()]
        public void Part2Example4Test()
        {
            var instance = new Day10(File.ReadAllText("inputs/day10-example4.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(8, answer);
        }
        [TestMethod()]
        public void Part2Example5Test()
        {
            var instance = new Day10(File.ReadAllText("inputs/day10-example5.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(10, answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day10(File.ReadAllText("inputs/day10-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(449, answer);
        }
    }
}