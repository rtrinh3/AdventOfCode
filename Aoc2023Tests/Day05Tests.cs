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
    public class Day05Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day05(File.ReadAllText("day05-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(35L, answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day05(File.ReadAllText("day05-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(226172555L, answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day05(File.ReadAllText("day05-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(46L, answer);
        }

        // 2 minute timeout
        // This takes about 1.8 minutes in Debug, 40 seconds in Release
        [TestMethod(), Timeout(120_000)]
        public void Part2InputTest()
        {
            var instance = new Day05(File.ReadAllText("day05-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(47909639L, answer);
        }
    }
}