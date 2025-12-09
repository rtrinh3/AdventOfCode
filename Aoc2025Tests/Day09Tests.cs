namespace Aoc2025.Tests;

// https://adventofcode.com/2025/day/9
// --- Day 9: Movie Theater ---
[TestClass()]
public class Day09Tests
{
    [TestMethod()]
    public void Day09_Part1_Example_Test()
    {
        var instance = new Day09(File.ReadAllText("day09-example.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("50", answer);
    }

    [TestMethod()]
    public void Day09_Part1_Input_Test()
    {
        var instance = new Day09(File.ReadAllText("day09-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("4725826296", answer);
    }

    [TestMethod()]
    public void Day09_Part2_Example_Test()
    {
        var instance = new Day09(File.ReadAllText("day09-example.txt"));
        var answer = instance.Part2();
        Assert.Inconclusive(answer);
        // Assert.AreEqual("EXPECTED_ANSWER", answer);
    }

    [TestMethod()]
    public void Day09_Part2_Input_Test()
    {
        var instance = new Day09(File.ReadAllText("day09-input.txt"));
        var answer = instance.Part2();
        Assert.Inconclusive(answer);
        // Assert.AreEqual("EXPECTED_ANSWER", answer);
    }
}