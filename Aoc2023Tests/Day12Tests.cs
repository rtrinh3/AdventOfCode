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
    public class Day12Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day12(File.ReadAllText("inputs/day12-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(21, answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day12(File.ReadAllText("inputs/day12-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(7204, answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day12(File.ReadAllText("inputs/day12-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(525152, answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day12(File.ReadAllText("inputs/day12-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(1672318386674, answer);
        }
    }
}