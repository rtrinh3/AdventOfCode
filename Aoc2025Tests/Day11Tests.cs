namespace Aoc2025.Tests;

// https://adventofcode.com/2025/day/11
// --- Day 11: Reactor ---
[TestClass()]
public class Day11Tests
{
    [TestMethod()]
    public void Day11_Part1_Example_Test()
    {
        var instance = new Day11(File.ReadAllText("day11-example.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("5", answer);
    }

    [TestMethod()]
    public void Day11_Part1_Input_Test()
    {
        var instance = new Day11(File.ReadAllText("day11-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("543", answer);
    }

    [TestMethod()]
    public void Day11_Part2_Example_Test()
    {
        var instance = new Day11(File.ReadAllText("day11-example.txt"));
        var answer = instance.Part2();
        Assert.Inconclusive(answer);
        // Assert.AreEqual("EXPECTED_ANSWER", answer);
    }

    [TestMethod()]
    public void Day11_Part2_Input_Test()
    {
        var instance = new Day11(File.ReadAllText("day11-input.txt"));
        var answer = instance.Part2();
        Assert.Inconclusive(answer);
        // Assert.AreEqual("EXPECTED_ANSWER", answer);
    }
}