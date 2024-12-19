namespace Aoc2024.Tests;

[TestClass()]
public class Day19Tests
{
    [TestMethod()]
    public void Part1ExampleTest()
    {
        var instance = new Day19(File.ReadAllText("day19-example.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("6", answer);
    }
    [TestMethod()]
    public void Part1InputTest()
    {
        var instance = new Day19(File.ReadAllText("day19-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("365", answer);
    }
    [TestMethod()]
    public void Part2ExampleTest()
    {
        var instance = new Day19(File.ReadAllText("day19-example.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("16", answer);
    }
    [TestMethod()]
    public void Part2InputTest()
    {
        var instance = new Day19(File.ReadAllText("day19-input.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("730121486795169", answer);
    }
}