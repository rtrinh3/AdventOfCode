namespace Aoc2025.Tests;

// https://adventofcode.com/2025/day/10
// --- Day 10: Factory ---
[TestClass()]
public class Day10Tests
{
    [TestMethod()]
    public void Day10_Part1_Example_Test()
    {
        var instance = new Day10(File.ReadAllText("day10-example.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("7", answer);
    }

    [TestMethod()]
    public void Day10_Part1_Input_Test()
    {
        var instance = new Day10(File.ReadAllText("day10-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("535", answer);
    }

    [TestMethod()]
    public void Day10_Part2_Example_Test()
    {
        var instance = new Day10(File.ReadAllText("day10-example.txt"));
        var answer = instance.Part2();
        Assert.Inconclusive(answer);
        // Assert.AreEqual("EXPECTED_ANSWER", answer);
    }

    [TestMethod()]
    public void Day10_Part2_Input_Test()
    {
        var instance = new Day10(File.ReadAllText("day10-input.txt"));
        var answer = instance.Part2();
        Assert.Inconclusive(answer);
        // Assert.AreEqual("EXPECTED_ANSWER", answer);
    }
}