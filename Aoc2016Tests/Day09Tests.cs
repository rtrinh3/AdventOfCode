namespace Aoc2016.Tests;

// https://adventofcode.com/2016/day/9
// --- Day 9: Explosives in Cyberspace ---
[TestClass()]
public class Day09Tests
{
    [TestMethod()]
    public void Day09_Part1_Example1_Test()
    {
        var instance = new Day09(File.ReadAllText("inputs/day09-example1.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("6", answer);
    }
    [TestMethod()]
    public void Day09_Part1_Example2_Test()
    {
        var instance = new Day09(File.ReadAllText("inputs/day09-example2.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("7", answer);
    }
    [TestMethod()]
    public void Day09_Part1_Example3_Test()
    {
        var instance = new Day09(File.ReadAllText("inputs/day09-example3.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("9", answer);
    }
    [TestMethod()]
    public void Day09_Part1_Example4_Test()
    {
        var instance = new Day09(File.ReadAllText("inputs/day09-example4.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("11", answer);
    }
    [TestMethod()]
    public void Day09_Part1_Example5_Test()
    {
        var instance = new Day09(File.ReadAllText("inputs/day09-example5.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("6", answer);
    }
    [TestMethod()]
    public void Day09_Part1_Example6_Test()
    {
        var instance = new Day09(File.ReadAllText("inputs/day09-example6.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("18", answer);
    }
    [TestMethod()]
    public void Day09_Part1_Input_Test()
    {
        var instance = new Day09(File.ReadAllText("inputs/day09-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("112830", answer);
    }

    [TestMethod()]
    public void Day09_Part2_Example3_Test()
    {
        var instance = new Day09(File.ReadAllText("inputs/day09-example3.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("9", answer);
    }
    [TestMethod()]
    public void Day09_Part2_Example6_Test()
    {
        var instance = new Day09(File.ReadAllText("inputs/day09-example6.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("20", answer);
    }
    [TestMethod()]
    public void Day09_Part2_Example7_Test()
    {
        var instance = new Day09(File.ReadAllText("inputs/day09-example7.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("241920", answer);
    }
    [TestMethod()]
    public void Day09_Part2_Example8_Test()
    {
        var instance = new Day09(File.ReadAllText("inputs/day09-example8.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("445", answer);
    }
    [TestMethod()]
    public void Day09_Part2_Input_Test()
    {
        var instance = new Day09(File.ReadAllText("inputs/day09-input.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("10931789799", answer);
    }
}