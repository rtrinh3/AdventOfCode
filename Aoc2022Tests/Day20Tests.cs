﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2022.Tests
{
    [TestClass()]
    public class Day20Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day20(File.ReadAllText("day20-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("3", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day20(File.ReadAllText("day20-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("2827", answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day20(File.ReadAllText("day20-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("1623178306", answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day20(File.ReadAllText("day20-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("7834270093909", answer);
        }
    }
}