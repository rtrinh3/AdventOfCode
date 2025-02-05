namespace Aoc2015.Tests;

// https://adventofcode.com/2015/day/22
// --- Day 22: Wizard Simulator 20XX ---
[TestClass()]
public class Day22Tests
{
    [TestMethod()]
    public void Part1Example1Test()
    {
        var instance = new Day22(File.ReadAllText("day22-example1.txt"));
        var answer = instance.CostToWinBattle(10, 250);
        Assert.AreEqual(173 + 53, answer);
    }
    [TestMethod()]
    public void Part1Example2Test()
    {
        var instance = new Day22(File.ReadAllText("day22-example2.txt"));
        var answer = instance.CostToWinBattle(10, 250);
        Assert.AreEqual(229 + 113 + 73 + 173 + 53, answer);
    }

    [TestMethod()]
    public void Part1InputTest()
    {
        var instance = new Day22(File.ReadAllText("day22-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("1269", answer);
    }
}