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
    public class Day24Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day24(File.ReadAllText("day24-example.txt"));
            var answer = instance.DoPart1(7, 27);
            Assert.AreEqual(2, answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day24(File.ReadAllText("day24-input.txt"));
            var answer = instance.DoPart1(200000000000000, 400000000000000);
            Assert.AreEqual(12938, answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day24(File.ReadAllText("day24-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(47, answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day24(File.ReadAllText("day24-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(976976197397181, answer);
        }
    }
}