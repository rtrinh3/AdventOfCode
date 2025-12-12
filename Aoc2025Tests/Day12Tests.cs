namespace Aoc2025.Tests;

// https://adventofcode.com/2025/day/12
// --- Day 12: Christmas Tree Farm ---
[TestClass()]
public class Day12Tests
{
    [TestMethod()]
    public void Day12_Part1_Example_Test()
    {
        var instance = new Day12(File.ReadAllText("day12-example.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("2", answer);
    }

    [TestMethod()]
    public void Day12_Part1_Input_Test()
    {
        var instance = new Day12(File.ReadAllText("day12-input.txt"));
        var answer = instance.Part1();
        Assert.Inconclusive(answer);
        // Assert.AreEqual("EXPECTED_ANSWER", answer);
    }

    [TestMethod()]
    public void Day12_Part2_Example_Test()
    {
        var instance = new Day12(File.ReadAllText("day12-example.txt"));
        var answer = instance.Part2();
        Assert.Inconclusive(answer);
        // Assert.AreEqual("EXPECTED_ANSWER", answer);
    }

    [TestMethod()]
    public void Day12_Part2_Input_Test()
    {
        var instance = new Day12(File.ReadAllText("day12-input.txt"));
        var answer = instance.Part2();
        Assert.Inconclusive(answer);
        // Assert.AreEqual("EXPECTED_ANSWER", answer);
    }
}