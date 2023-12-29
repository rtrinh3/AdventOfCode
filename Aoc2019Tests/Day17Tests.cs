﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2019.Tests
{
    [TestClass()]
    public class Day17Tests
    {
        [TestMethod()]
        public void Part1ExampleTest()
        {
            var instance = new Day17(File.ReadAllText("day17-example.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("", answer);
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day17(File.ReadAllText("day17-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("", answer);
        }

        [TestMethod()]
        public void Part2ExampleTest()
        {
            var instance = new Day17(File.ReadAllText("day17-example.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("", answer);
        }
        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day17(File.ReadAllText("day17-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("", answer);
        }
    }
}