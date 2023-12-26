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
    public class Day25Tests
    {
        [TestMethod()]
        public void ExampleTest()
        {
            var instance = new Day25(File.ReadAllText("day25-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(54, answer);
        }
        [TestMethod()]
        public void InputTest()
        {
            var instance = new Day25(File.ReadAllText("day25-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(555856, answer);
        }
    }
}