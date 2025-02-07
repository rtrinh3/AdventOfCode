namespace Aoc2015.Tests;

// https://adventofcode.com/2015/day/24
// --- Day 24: It Hangs in the Balance ---
[TestClass()]
public class Day24Tests
{
    [TestMethod()]
    public void Part1ExampleTest()
    {
        var instance = new Day24(File.ReadAllText("day24-example.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("99", answer);
    }
    [TestMethod()]
    public void Part1InputTest()
    {
        var instance = new Day24(File.ReadAllText("day24-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("10439961859", answer);
    }

    [TestMethod()]
    public void Part2ExampleTest()
    {
        var instance = new Day24(File.ReadAllText("day24-example.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("44", answer);
    }
    [TestMethod()]
    public void Part2InputTest()
    {
        var instance = new Day24(File.ReadAllText("day24-input.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("72050269", answer);
    }
}
