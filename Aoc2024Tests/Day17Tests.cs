namespace Aoc2024.Tests;

// https://adventofcode.com/2024/day/17
// --- Day 17: Chronospatial Computer ---
[TestClass()]
public class Day17Tests
{
    [TestMethod()]
    public void Part1Example6Test()
    {
        var instance = new Day17(File.ReadAllText("day17-example6.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("4,6,3,5,6,3,5,2,1,0", answer);
    }
    [TestMethod()]
    public void Part1InputTest()
    {
        var instance = new Day17(File.ReadAllText("day17-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("1,3,5,1,7,2,5,1,6", answer);
    }

    [TestMethod()]
    public void Part2Example7Test()
    {
        var instance = new Day17(File.ReadAllText("day17-example7.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("117440", answer);
    }
    [TestMethod()]
    public void Part2InputTest()
    {
        var instance = new Day17(File.ReadAllText("day17-input.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("236555997372013", answer);
    }
}