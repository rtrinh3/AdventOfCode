using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aoc2023;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023.Tests
{
    // https://adventofcode.com/2023/day/22
    [TestClass()]
    public class Day22Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day22(File.ReadAllText("inputs/day22-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(5, answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day22(File.ReadAllText("inputs/day22-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(395, answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day22(File.ReadAllText("inputs/day22-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(7, answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day22(File.ReadAllText("inputs/day22-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(64714, answer);
        }
    }
}