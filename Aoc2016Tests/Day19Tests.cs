namespace Aoc2016.Tests;

// https://adventofcode.com/2016/day/19
// --- Day 19: An Elephant Named Joseph ---
[TestClass()]
public class Day19Tests
{
    [TestMethod()]
    public void Day19_Part1_Example_Test()
    {
        var instance = new Day19(File.ReadAllText("day19-example.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("3", answer);
    }
    [TestMethod()]
    public void Day19_Part1_Input_Test()
    {
        var instance = new Day19(File.ReadAllText("day19-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("1834471", answer);
    }

    [TestMethod()]
    public void Day19_Part2_Example_Test()
    {
        var instance = new Day19(File.ReadAllText("day19-example.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("2", answer);
    }
    [TestMethod()]
    public void Day19_Part2_Input_Test()
    {
        var instance = new Day19(File.ReadAllText("day19-input.txt"));
        var answer = instance.Part2();
        //Assert.AreEqual("1834471", answer);
        Assert.Inconclusive(answer);
    }
}
