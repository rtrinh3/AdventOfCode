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
    public class Day21Tests
    {
        [TestMethod()]
        public void Part1Test()
        {
            var instance = new Day21(File.ReadAllText("day21-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(3591, answer);
        }
        [TestMethod()]
        public void Part2Test()
        {
            var instance = new Day21(File.ReadAllText("day21-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(598044246091826, answer);
        }
    }
}