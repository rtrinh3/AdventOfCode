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
    public class Day04Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day04(File.ReadAllText("inputs/day04-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(13, answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day04(File.ReadAllText("inputs/day04-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(32609, answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day04(File.ReadAllText("inputs/day04-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(30, answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day04(File.ReadAllText("inputs/day04-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(14624680, answer);
        }
    }
}