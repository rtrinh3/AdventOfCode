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
    public class Day06Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day06(File.ReadAllText("day06-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(288, answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day06(File.ReadAllText("day06-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(449820, answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day06(File.ReadAllText("day06-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(71503, answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day06(File.ReadAllText("day06-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(42250895, answer);
        }
    }
}