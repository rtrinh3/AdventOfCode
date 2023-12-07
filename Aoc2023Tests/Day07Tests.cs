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
    public class Day07Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day07(File.ReadAllText("day07-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(6440, answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day07(File.ReadAllText("day07-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(252295678, answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day07(File.ReadAllText("day07-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(5905, answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day07(File.ReadAllText("day07-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(250577259, answer);
        }
    }
}