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
    public class Day23Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day23(File.ReadAllText("day23-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(94, answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day23(File.ReadAllText("day23-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(2178, answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day23(File.ReadAllText("day23-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(154, answer);
        }
        [TestMethod(), Timeout(16_000)]
        public void Part2InputTest()
        {
            var instance = new Day23(File.ReadAllText("day23-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(6486, answer);
        }
    }
}