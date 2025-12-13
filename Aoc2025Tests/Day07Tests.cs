namespace Aoc2025.Tests;

// https://adventofcode.com/2025/day/7
// --- Day 7: Laboratories ---
[TestClass()]
public class Day07Tests
{
    [TestMethod()]
    public void Day07_Part1_Example_Test()
    {
        var instance = new Day07(File.ReadAllText("inputs/day07-example.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("21", answer);
    }

    [TestMethod()]
    public void Day07_Part1_Input_Test()
    {
        var instance = new Day07(File.ReadAllText("inputs/day07-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("1622", answer);
    }

    [TestMethod()]
    public void Day07_Part2_Example_Test()
    {
        var instance = new Day07(File.ReadAllText("inputs/day07-example.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("40", answer);
    }

    [TestMethod()]
    public void Day07_Part2_Input_Test()
    {
        var instance = new Day07(File.ReadAllText("inputs/day07-input.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("10357305916520", answer);
    }
}