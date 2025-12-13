using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aoc2023;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023.Tests
{
    // https://adventofcode.com/2023/day/17
    [TestClass()]
    public class Day17Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day17(File.ReadAllText("inputs/day17-example1.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(102, answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day17(File.ReadAllText("inputs/day17-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(635, answer);
        }

        [TestMethod()]
        public void Part2Example1Test()
        {
            var instance = new Day17(File.ReadAllText("inputs/day17-example1.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(94, answer);
        }
        [TestMethod()]
        public void Part2Example2Test()
        {
            var instance = new Day17(File.ReadAllText("inputs/day17-example2.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(71, answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day17(File.ReadAllText("inputs/day17-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(734, answer);
        }
    }
}