namespace Aoc2016.Tests;

// https://adventofcode.com/2016/day/18
// --- Day 18: Like a Rogue ---
[TestClass()]
public class Day18Tests
{
    [TestMethod()]
    public void Day18_Part1_Example_Test()
    {
        var instance = new Day18(File.ReadAllText("day18-example.txt"));
        var answer = instance.DoPuzzle(10);
        Assert.AreEqual(38, answer);
    }
    [TestMethod()]
    public void Day18_Part1_Input_Test()
    {
        var instance = new Day18(File.ReadAllText("day18-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("2013", answer);
    }

    [TestMethod()]
    public void Day18_Part2_Input_Test()
    {
        var instance = new Day18(File.ReadAllText("day18-input.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("20006289", answer);
    }
}