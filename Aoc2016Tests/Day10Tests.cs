namespace Aoc2016.Tests;

// https://adventofcode.com/2016/day/10
// --- Day 10: Balance Bots ---
[TestClass()]
public class Day10Tests
{
    [TestMethod()]
    public void Day10_Part1_Input_Test()
    {
        var instance = new Day10(File.ReadAllText("inputs/day10-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("157", answer);
    }

    [TestMethod()]
    public void Day10_Part2_Input_Test()
    {
        var instance = new Day10(File.ReadAllText("inputs/day10-input.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("1085", answer);
    }
}
