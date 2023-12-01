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
    public class Day01Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day01(File.ReadAllText("day01-example1.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(142, answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day01(File.ReadAllText("day01-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(53334, answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day01(File.ReadAllText("day01-example2.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(281, answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day01(File.ReadAllText("day01-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(52834, answer);
        }
    }
}