namespace Aoc2016.Tests;

// https://adventofcode.com/2016/day/5
// --- Day 5: How About a Nice Game of Chess? ---
[TestClass()]
public class Day05Tests
{
    [TestMethod()]
    public void Day05_Part1_Example_Test()
    {
        var instance = new Day05(File.ReadAllText("inputs/day05-example.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("18f47a30", answer);
    }
    [TestMethod()]
    public void Day05_Part1_Input_Test()
    {
        var instance = new Day05(File.ReadAllText("inputs/day05-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("f97c354d", answer);
    }

    [TestMethod()]
    public void Day05_Part2_Example_Test()
    {
        var instance = new Day05(File.ReadAllText("inputs/day05-example.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("05ace8e3", answer);
    }
    [TestMethod()]
    public void Day05_Part2_Input_Test()
    {
        var instance = new Day05(File.ReadAllText("inputs/day05-input.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("863dde27", answer);
    }
}
