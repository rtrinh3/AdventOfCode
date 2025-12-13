namespace Aoc2016.Tests;

// https://adventofcode.com/2016/day/4
// --- Day 4: Security Through Obscurity ---
[TestClass()]
public class Day04Tests
{
    [TestMethod()]
    public void Day04_Part1_Example_Test()
    {
        var instance = new Day04(File.ReadAllText("inputs/day04-example.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("1514", answer);
    }
    [TestMethod()]
    public void Day04_Part1_Input_Test()
    {
        var instance = new Day04(File.ReadAllText("inputs/day04-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("278221", answer);
    }

    [TestMethod()]
    public void Day04_Part2_Input_Test()
    {
        var instance = new Day04(File.ReadAllText("inputs/day04-input.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("267", answer);
    }
}
