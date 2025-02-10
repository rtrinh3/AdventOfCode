namespace Aoc2016.Tests;

// https://adventofcode.com/2016/day/2
// --- Day 2: Bathroom Security ---
[TestClass()]
public class Day02Tests
{
    [TestMethod()]
    public void Day02_Part1_Example_Test()
    {
        var instance = new Day02(File.ReadAllText("day02-example.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("1985", answer);
    }
    [TestMethod()]
    public void Day02_Part1_Input_Test()
    {
        var instance = new Day02(File.ReadAllText("day02-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("19636", answer);
    }

    [TestMethod()]
    public void Day02_Part2_Example_Test()
    {
        var instance = new Day02(File.ReadAllText("day02-example.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("5DB3", answer);
    }
    [TestMethod()]
    public void Day02_Part2_Input_Test()
    {
        var instance = new Day02(File.ReadAllText("day02-input.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("3CC43", answer);
    }
}
