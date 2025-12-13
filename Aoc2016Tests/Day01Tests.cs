namespace Aoc2016.Tests;

// https://adventofcode.com/2016/day/1
// --- Day 1: No Time for a Taxicab ---
[TestClass()]
public class Day01Tests
{
    [TestMethod()]
    public void Day01_Part1_Example1_Test()
    {
        var instance = new Day01(File.ReadAllText("inputs/day01-example1.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("5", answer);
    }
    [TestMethod()]
    public void Day01_Part1_Example2_Test()
    {
        var instance = new Day01(File.ReadAllText("inputs/day01-example2.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("2", answer);
    }
    [TestMethod()]
    public void Day01_Part1_Example3_Test()
    {
        var instance = new Day01(File.ReadAllText("inputs/day01-example3.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("12", answer);
    }
    [TestMethod()]
    public void Day01_Part1_Input_Test()
    {
        var instance = new Day01(File.ReadAllText("inputs/day01-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("209", answer);
    }

    [TestMethod()]
    public void Day01_Part2_Example4_Test()
    {
        var instance = new Day01(File.ReadAllText("inputs/day01-example4.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("4", answer);
    }
    [TestMethod()]
    public void Day01_Part2_Input_Test()
    {
        var instance = new Day01(File.ReadAllText("inputs/day01-input.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("136", answer);
    }
}
