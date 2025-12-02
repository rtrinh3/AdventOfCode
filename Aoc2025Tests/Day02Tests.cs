namespace Aoc2025.Tests;

// https://adventofcode.com/2025/day/2
// --- Day 2: Gift Shop ---
[TestClass()]
public class Day02Tests
{
    [TestMethod()]
    public void Day02_Part1_Example_Test()
    {
        var instance = new Day02(File.ReadAllText("day02-example.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("1227775554", answer);
    }

    [TestMethod()]
    public void Day02_Part1_Input_Test()
    {
        var instance = new Day02(File.ReadAllText("day02-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("19128774598", answer);
    }

    [TestMethod()]
    public void Day02_Part2_Example_Test()
    {
        var instance = new Day02(File.ReadAllText("day02-example.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("4174379265", answer);
    }

    [TestMethod()]
    public void Day02_Part2_Input_Test()
    {
        var instance = new Day02(File.ReadAllText("day02-input.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("21932258645", answer);
    }
}