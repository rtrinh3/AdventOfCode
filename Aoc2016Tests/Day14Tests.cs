namespace Aoc2016.Tests;

// https://adventofcode.com/2016/day/14
// --- Day 14: One-Time Pad ---
[TestClass()]
public class Day14Tests
{
    [TestMethod()]
    public void Day14_Part1_Example_Test()
    {
        var instance = new Day14(File.ReadAllText("day14-example.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("22728", answer);
    }
    [TestMethod()]
    public void Day14_Part1_Input_Test()
    {
        var instance = new Day14(File.ReadAllText("day14-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("18626", answer);
    }

    [TestMethod(), Timeout(60_000)]
    public void Day14_Part2_Example_Test()
    {
        var instance = new Day14(File.ReadAllText("day14-example.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("22551", answer);
    }
    [TestMethod(), Timeout(60_000)]
    public void Day14_Part2_Input_Test()
    {
        var instance = new Day14(File.ReadAllText("day14-input.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("20092", answer);
    }
}
