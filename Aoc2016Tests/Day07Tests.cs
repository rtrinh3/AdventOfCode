namespace Aoc2016.Tests;

// https://adventofcode.com/2016/day/7
// --- Day 7: Internet Protocol Version 7 ---
[TestClass()]
public class Day07Tests
{
    [TestMethod()]
    public void Day07_Part1_Example1_Test()
    {
        var instance = new Day07(File.ReadAllText("inputs/day07-example1.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("2", answer);
    }
    [TestMethod()]
    public void Day07_Part1_Input_Test()
    {
        var instance = new Day07(File.ReadAllText("inputs/day07-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("105", answer);
    }

    [TestMethod()]
    public void Day07_Part2_Example2_Test()
    {
        var instance = new Day07(File.ReadAllText("inputs/day07-example2.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("3", answer);
    }
    [TestMethod()]
    public void Day07_Part2_Input_Test()
    {
        var instance = new Day07(File.ReadAllText("inputs/day07-input.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("258", answer);
    }
}
