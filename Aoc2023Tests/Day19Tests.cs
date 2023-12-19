using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aoc2023;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023.Tests
{
    // https://adventofcode.com/2023/day/19
    [TestClass()]
    public class Day19Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day19(File.ReadAllText("day19-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(19114, answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day19(File.ReadAllText("day19-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(575412, answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day19(File.ReadAllText("day19-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(167409079868000, answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day19(File.ReadAllText("day19-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(126107942006821, answer);
        }
    }
}