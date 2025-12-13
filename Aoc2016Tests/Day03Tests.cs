namespace Aoc2016.Tests;

// https://adventofcode.com/2016/day/3
// --- Day 3: Squares With Three Sides ---
[TestClass()]
public class Day03Tests
{
    [TestMethod()]
    public void Day03_Part1_Example_Test()
    {
        var instance = new Day03(File.ReadAllText("inputs/day03-example.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("0", answer);
    }

    [TestMethod()]
    public void Day03_Part1_Input_Test()
    {
        var instance = new Day03(File.ReadAllText("inputs/day03-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("993", answer);
    }

    [TestMethod()]
    public void Day03_Part2_Input_Test()
    {
        var instance = new Day03(File.ReadAllText("inputs/day03-input.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("1849", answer);
    }
}
