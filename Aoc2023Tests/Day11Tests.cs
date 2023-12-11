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
    public class Day11Tests
    {
        [TestMethod()]
        public void Part1Test()
        {
            var instance = new Day11(File.ReadAllText("day11-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(10228230, answer);
        }

        [TestMethod()]
        public void Part2Test()
        {
            var instance = new Day11(File.ReadAllText("day11-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(447073334102, answer);
        }

        [TestMethod()]
        public void ExampleTest()
        {
            var instance = new Day11(File.ReadAllText("day11-example.txt"));
            Assert.AreEqual(374, instance.Part1());
            Assert.AreEqual(1030, instance.DoPuzzle(10));
            Assert.AreEqual(8410, instance.DoPuzzle(100));
        }
    }
}