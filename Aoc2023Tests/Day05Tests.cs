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
    public class Day05Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day05(File.ReadAllText("inputs/day05-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(35L, answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day05(File.ReadAllText("inputs/day05-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(226172555L, answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day05(File.ReadAllText("inputs/day05-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(46L, answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day05(File.ReadAllText("inputs/day05-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(47909639L, answer);
        }
    }
}