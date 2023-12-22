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
    public class Day22Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day22(File.ReadAllText("day22-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(5, answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day22(File.ReadAllText("day22-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(395, answer);
        }
    }
}