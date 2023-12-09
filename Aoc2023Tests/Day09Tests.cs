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
    public class Day09Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day09(File.ReadAllText("day09-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(114, answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day09(File.ReadAllText("day09-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(1974913025, answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day09(File.ReadAllText("day09-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(2, answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day09(File.ReadAllText("day09-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(884, answer);
        }
    }
}