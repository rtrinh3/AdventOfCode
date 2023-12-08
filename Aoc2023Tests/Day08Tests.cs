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
    public class Day08Tests
    {
        [TestMethod()]
        public void Part1Example1Test()
        {
            var instance = new Day08(File.ReadAllText("day08-example1.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(2, answer);
        }
        [TestMethod()]
        public void Part1Example2Test()
        {
            var instance = new Day08(File.ReadAllText("day08-example2.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(6, answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day08(File.ReadAllText("day08-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual(20093, answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day08(File.ReadAllText("day08-example3.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(6, answer);
            var bruteAnswer = instance.Part2BruteForce();
            Assert.AreEqual(bruteAnswer, answer);
        }
        [TestMethod(), Timeout(1000)]
        public void Part2InputTest()
        {
            var instance = new Day08(File.ReadAllText("day08-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual(22103062509257, answer);
        }
    }
}