namespace Aoc2024.Tests;

// https://adventofcode.com/2024/day/24
// --- Day 24: Crossed Wires ---
[TestClass()]
public class Day24Tests
{
    [TestMethod()]
    public void Part1Example1Test()
    {
        var instance = new Day24(File.ReadAllText("day24-example1.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("4", answer);
    }
    [TestMethod()]
    public void Part1Example2Test()
    {
        var instance = new Day24(File.ReadAllText("day24-example2.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("2024", answer);
    }
    [TestMethod()]
    public void Part1InputTest()
    {
        var instance = new Day24(File.ReadAllText("day24-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("59364044286798", answer);
    }
}
