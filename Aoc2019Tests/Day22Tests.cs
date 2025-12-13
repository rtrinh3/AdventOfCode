using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2019.Tests
{
    [TestClass()]
    public class Day22Tests
    {
        [TestMethod()]
        public void Part1Example1Test()
        {
            List<int> expectedDeck = [0, 3, 6, 9, 2, 5, 8, 1, 4, 7];
            List<int> expectedPositions = Enumerable.Range(0, 10).Select(card => expectedDeck.IndexOf(card)).ToList();

            var instance = new Day22(File.ReadAllText("inputs/day22-example1.txt"));
            List<int> actualPositions = Enumerable.Range(0, 10).Select(card => instance.DoPart1(10, card)).ToList();

            Assert.IsTrue(expectedPositions.SequenceEqual(actualPositions));
        }
        [TestMethod()]
        public void Part1Example2Test()
        {
            List<int> expectedDeck = [3, 0, 7, 4, 1, 8, 5, 2, 9, 6];
            List<int> expectedPositions = Enumerable.Range(0, 10).Select(card => expectedDeck.IndexOf(card)).ToList();

            var instance = new Day22(File.ReadAllText("inputs/day22-example2.txt"));
            List<int> actualPositions = Enumerable.Range(0, 10).Select(card => instance.DoPart1(10, card)).ToList();

            Assert.IsTrue(expectedPositions.SequenceEqual(actualPositions));
        }
        [TestMethod()]
        public void Part1Example3Test()
        {
            List<int> expectedDeck = [6, 3, 0, 7, 4, 1, 8, 5, 2, 9];
            List<int> expectedPositions = Enumerable.Range(0, 10).Select(card => expectedDeck.IndexOf(card)).ToList();

            var instance = new Day22(File.ReadAllText("inputs/day22-example3.txt"));
            List<int> actualPositions = Enumerable.Range(0, 10).Select(card => instance.DoPart1(10, card)).ToList();

            Assert.IsTrue(expectedPositions.SequenceEqual(actualPositions));
        }
        [TestMethod()]
        public void Part1Example4Test()
        {
            List<int> expectedDeck = [9, 2, 5, 8, 1, 4, 7, 0, 3, 6];
            List<int> expectedPositions = Enumerable.Range(0, 10).Select(card => expectedDeck.IndexOf(card)).ToList();

            var instance = new Day22(File.ReadAllText("inputs/day22-example4.txt"));
            List<int> actualPositions = Enumerable.Range(0, 10).Select(card => instance.DoPart1(10, card)).ToList();

            Assert.IsTrue(expectedPositions.SequenceEqual(actualPositions));
        }
        [TestMethod()]
        public void Part1InputTest()
        {
            var instance = new Day22(File.ReadAllText("inputs/day22-input.txt"));
            var answer = instance.Part1();
            Assert.AreEqual("3293", answer);
        }

        [TestMethod()]
        public void Part2InputTest()
        {
            var instance = new Day22(File.ReadAllText("inputs/day22-input.txt"));
            var answer = instance.Part2();
            Assert.AreEqual("54168121233945", answer);
        }
    }
}