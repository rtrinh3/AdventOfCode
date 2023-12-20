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
    public class Day20Tests
    {
        [TestMethod()]
        public void Part1Example1Test()
        {
            var instance = new Day20(File.ReadAllText("day20-example1.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(32000000, answer);
        }
        [TestMethod()]
        public void Part1Example2Test()
        {
            var instance = new Day20(File.ReadAllText("day20-example2.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(11687500, answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day20(File.ReadAllText("day20-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(818723272, answer);
        }

        [TestMethod(), Timeout(1000)]
        public void Part2Test()
        {
            var instance = new Day20(File.ReadAllText("day20-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(243902373381257, answer);
        }
    }
}