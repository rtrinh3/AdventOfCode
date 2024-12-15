namespace Aoc2024.Tests;

// https://adventofcode.com/2024/day/15
// --- Day 15: Warehouse Woes ---
[TestClass()]
public class Day15Tests
{
    [TestMethod()]
    public void Part1Example1Test()
    {
        var instance = new Day15(File.ReadAllText("day15-example1.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("10092", answer);
    }
    [TestMethod()]
    public void Part1Example2Test()
    {
        var instance = new Day15(File.ReadAllText("day15-example2.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("2028", answer);
    }
    [TestMethod()]
    public void Part1InputTest()
    {
        var instance = new Day15(File.ReadAllText("day15-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("1526673", answer);
    }

    [TestMethod()]
    public void Part2Example1Test()
    {
        var instance = new Day15(File.ReadAllText("day15-example1.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("9021", answer);
    }
    [TestMethod()]
    public void Part2InputTest()
    {
        var instance = new Day15(File.ReadAllText("day15-input.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("1535509", answer);
    }
}