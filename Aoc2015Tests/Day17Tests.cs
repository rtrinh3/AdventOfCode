namespace Aoc2015.Tests;

// https://adventofcode.com/2015/day/17
// --- Day 17: No Such Thing as Too Much ---
[TestClass()]
public class Day17Tests
{
    [TestMethod()]
    public void Part1ExampleTest()
    {
        var instance = new Day17(File.ReadAllText("day17-example.txt"));
        var answer = instance.DoPart1(25);
        Assert.AreEqual(4, answer);
    }
    [TestMethod()]
    public void Part1InputTest()
    {
        var instance = new Day17(File.ReadAllText("day17-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("654", answer);
    }

    [TestMethod()]
    public void Part2ExampleTest()
    {
        var instance = new Day17(File.ReadAllText("day17-example.txt"));
        var answer = instance.DoPart2(25);
        Assert.AreEqual(3, answer);
    }
    [TestMethod()]
    public void Part2InputTest()
    {
        var instance = new Day17(File.ReadAllText("day17-input.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("57", answer);
    }
}