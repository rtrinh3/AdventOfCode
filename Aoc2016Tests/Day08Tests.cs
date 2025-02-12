namespace Aoc2016.Tests;

// https://adventofcode.com/2016/day/8
// --- Day 8: Two-Factor Authentication ---
[TestClass()]
public class Day08Tests
{
    [TestMethod()]
    public void Day08_Part1_Example_Test()
    {
        var instance = new Day08(File.ReadAllText("day08-example.txt"));
        var answer = instance.DoPart1(7, 3);
        Assert.AreEqual(6, answer);
    }
    [TestMethod()]
    public void Day08_Part1_Input_Test()
    {
        var instance = new Day08(File.ReadAllText("day08-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("115", answer);
    }

    [TestMethod()]
    public void Day08_Part2_Input_Test()
    {
        var instance = new Day08(File.ReadAllText("day08-input.txt"));
        var answer = instance.Part2();
        Assert.Inconclusive("\n" + answer);
    }
}