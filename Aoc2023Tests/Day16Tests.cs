using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aoc2023;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023.Tests
{
    // https://adventofcode.com/2023/day/16
    [TestClass()]
    public class Day16Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day16(File.ReadAllText("day16-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(46, answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day16(File.ReadAllText("day16-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(8098, answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day16(File.ReadAllText("day16-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(51, answer);
        }
        [TestMethod(), Timeout(1500)]
        public void Part2InputTest()
        {
            var instance = new Day16(File.ReadAllText("day16-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(8335, answer);
        }
    }
}