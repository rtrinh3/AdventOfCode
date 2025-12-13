namespace Aoc2025.Tests;

// https://adventofcode.com/2025/day/5
// --- Day 5: Cafeteria ---
[TestClass()]
public class Day05Tests
{
    [TestMethod()]
    public void Day05_Part1_Example_Test()
    {
        var instance = new Day05(File.ReadAllText("inputs/day05-example.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("3", answer);
    }

    [TestMethod()]
    public void Day05_Part1_Input_Test()
    {
        var instance = new Day05(File.ReadAllText("inputs/day05-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("601", answer);
    }

    [TestMethod()]
    public void Day05_Part2_Example_Test()
    {
        var instance = new Day05(File.ReadAllText("inputs/day05-example.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("14", answer);
    }

    [TestMethod()]
    public void Day05_Part2_Input_Test()
    {
        var instance = new Day05(File.ReadAllText("inputs/day05-input.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("367899984917516", answer);
    }
}