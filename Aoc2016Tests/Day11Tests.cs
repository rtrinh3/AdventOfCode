namespace Aoc2016.Tests;

// https://adventofcode.com/2016/day/11
// --- Day 11: Radioisotope Thermoelectric Generators ---
[TestClass()]
public class Day11Tests
{
    [TestMethod()]
    public void Day11_Part1_Example_Test()
    {
        var instance = new Day11(File.ReadAllText("day11-example.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("11", answer);
    }
    [TestMethod(), Timeout(15_000)]
    public void Day11_Part1_Input_Test()
    {
        var instance = new Day11(File.ReadAllText("day11-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("37", answer);
    }

    [TestMethod()]
    public void Day11_Part2_Input_Test()
    {
        var instance = new Day11(File.ReadAllText("day11-input.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("61", answer);
    }
}