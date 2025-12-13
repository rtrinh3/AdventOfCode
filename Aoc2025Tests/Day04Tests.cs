namespace Aoc2025.Tests;

// https://adventofcode.com/2025/day/4
// --- Day 4: Printing Department ---
[TestClass()]
public class Day04Tests
{
    [TestMethod()]
    public void Day04_Part1_Example_Test()
    {
        var instance = new Day04(File.ReadAllText("inputs/day04-example.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("13", answer);
    }

    [TestMethod()]
    public void Day04_Part1_Input_Test()
    {
        var instance = new Day04(File.ReadAllText("inputs/day04-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("1441", answer);
    }

    [TestMethod()]
    public void Day04_Part2_Example_Test()
    {
        var instance = new Day04(File.ReadAllText("inputs/day04-example.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("43", answer);
    }

    [TestMethod()]
    public void Day04_Part2_Input_Test()
    {
        var instance = new Day04(File.ReadAllText("inputs/day04-input.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("9050", answer);
    }
}