namespace Aoc2024.Tests;

// https://adventofcode.com/2024/day/16
// --- Day 16: Reindeer Maze ---
[TestClass()]
public class Day16Tests
{
    [TestMethod()]
    public void Part1Example1Test()
    {
        var instance = new Day16(File.ReadAllText("day16-example1.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("7036", answer);
    }
    [TestMethod()]
    public void Part1Example2Test()
    {
        var instance = new Day16(File.ReadAllText("day16-example2.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("11048", answer);
    }
    [TestMethod()]
    public void Part1InputTest()
    {
        var instance = new Day16(File.ReadAllText("day16-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("133584", answer);
    }

    [TestMethod()]
    public void Part2Example1Test()
    {
        var instance = new Day16(File.ReadAllText("day16-example1.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("45", answer);
    }
    [TestMethod()]
    public void Part2Example2Test()
    {
        var instance = new Day16(File.ReadAllText("day16-example2.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("64", answer);
    }
    [TestMethod()]
    public void Part2InputTest()
    {
        var instance = new Day16(File.ReadAllText("day16-input.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("622", answer);
    }
}