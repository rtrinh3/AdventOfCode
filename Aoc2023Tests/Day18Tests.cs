using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aoc2023;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023.Tests
{
    // https://adventofcode.com/2023/day/18
    [TestClass()]
    public class Day18Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day18(File.ReadAllText("day18-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(62, answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day18(File.ReadAllText("day18-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(50465, answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day18(File.ReadAllText("day18-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(952408144115, answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day18(File.ReadAllText("day18-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(82712746433310, answer);
        }
    }
}