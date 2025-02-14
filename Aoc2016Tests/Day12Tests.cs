namespace Aoc2016.Tests;

// https://adventofcode.com/2016/day/12
// --- Day 12: Leonardo's Monorail ---
[TestClass()]
public class Day12Tests
{
    [TestMethod()]
    public void Day12_Part1_Example_Test()
    {
        var instance = new Day12(File.ReadAllText("day12-example.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("42", answer);
    }
    [TestMethod()]
    public void Day12_Part1_Input_Test()
    {
        var instance = new Day12(File.ReadAllText("day12-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("317993", answer);
    }

    [TestMethod()]
    public void Day12_Part2_Input_Test()
    {
        var instance = new Day12(File.ReadAllText("day12-input.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("9227647", answer);
    }
}
