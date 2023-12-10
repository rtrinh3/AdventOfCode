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
    public class Day10Tests
    {
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day10(File.ReadAllText("day10-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(6806, answer);
        }

        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day10(File.ReadAllText("day10-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(449, answer);
        }
    }
}