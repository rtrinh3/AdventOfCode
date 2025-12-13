namespace Aoc2016.Tests;

// https://adventofcode.com/2016/day/24
// --- Day 24: Air Duct Spelunking ---
[TestClass()]
public class Day24Tests
{
    [TestMethod()]
    public void Day24_Part1_Example_Test()
    {
        var instance = new Day24(File.ReadAllText("inputs/day24-example.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("14", answer);
    }
    [TestMethod()]
    public void Day24_Part1_Input_Test()
    {
        var instance = new Day24(File.ReadAllText("inputs/day24-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("518", answer);
    }

    [TestMethod()]
    public void Day24_Part2_Input_Test()
    {
        var instance = new Day24(File.ReadAllText("inputs/day24-input.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("716", answer);
    }
}
