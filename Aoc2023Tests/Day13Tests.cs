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
    public class Day13Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day13(File.ReadAllText("inputs/day13-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(405, answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day13(File.ReadAllText("inputs/day13-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(30705, answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day13(File.ReadAllText("inputs/day13-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(400, answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day13(File.ReadAllText("inputs/day13-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(44615, answer);
        }
    }
}