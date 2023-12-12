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
            var instance = new Day12(File.ReadAllText("day12-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(21, answer);
        }
        [TestMethod(), Timeout(60_000)]
        public void Part1InputTest()
        {
            var instance = new Day12(File.ReadAllText("day12-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(7204, answer);
        }

        [TestMethod(), Timeout(10_000)]
        public void Part2ExampleTest()
        {
            var instance = new Day12(File.ReadAllText("day12-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(525152, answer);
        }
        [TestMethod(), Timeout(3_600_000)]
        public void Part2InputTest()
        {
            var instance = new Day12(File.ReadAllText("day12-input.txt"));
            var answer = instance.Part2();
            //Assert.AreEqual(884, answer);
            Assert.Inconclusive(answer.ToString());
        }
    }
}