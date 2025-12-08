namespace Aoc2025.Tests;

// https://adventofcode.com/2025/day/8
// --- Day 8: Playground ---
[TestClass()]
public class Day08Tests
{
    [TestMethod()]
    public void Day08_Part1_Example_Test()
    {
        var instance = new Day08(File.ReadAllText("day08-example.txt"));
        var answer = instance.DoPart1(10);
        Assert.AreEqual(40, answer);
    }

    [TestMethod()]
    public void Day08_Part1_Input_Test()
    {
        var instance = new Day08(File.ReadAllText("day08-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("96672", answer);
    }

    [TestMethod()]
    public void Day08_Part2_Example_Test()
    {
        var instance = new Day08(File.ReadAllText("day08-example.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("25272", answer);
    }

    [TestMethod()]
    public void Day08_Part2_Input_Test()
    {
        var instance = new Day08(File.ReadAllText("day08-input.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("22517595", answer);
    }
}