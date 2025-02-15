namespace Aoc2016.Tests;

// https://adventofcode.com/2016/day/15
// --- Day 15: Timing is Everything ---
[TestClass()]
public class Day15Tests
{
    [TestMethod()]
    public void Day15_Part1_Example_Test()
    {
        var instance = new Day15(File.ReadAllText("day15-example.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("5", answer);
    }
    [TestMethod()]
    public void Day15_Part1_Input_Test()
    {
        var instance = new Day15(File.ReadAllText("day15-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("121834", answer);
    }

    [TestMethod()]
    public void Day15_Part2_Input_Test()
    {
        var instance = new Day15(File.ReadAllText("day15-input.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("3208099", answer);
    }
}