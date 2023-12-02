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
    public class Day02Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day02(File.ReadAllText("day02-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(8, answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day02(File.ReadAllText("day02-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(2164, answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day02(File.ReadAllText("day02-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(2286, answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day02(File.ReadAllText("day02-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(69929, answer);
        }
    }
}