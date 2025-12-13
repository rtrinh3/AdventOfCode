namespace Aoc2016.Tests;

// https://adventofcode.com/2016/day/20
// --- Day 20: Firewall Rules ---
[TestClass()]
public class Day20Tests
{
    [TestMethod()]
    public void Day20_Part1_Example_Test()
    {
        var instance = new Day20(File.ReadAllText("inputs/day20-example.txt"), 9);
        var answer = instance.Part1();
        Assert.AreEqual("3", answer);
    }
    [TestMethod()]
    public void Day20_Part1_Input_Test()
    {
        var instance = new Day20(File.ReadAllText("inputs/day20-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("23923783", answer);
    }

    [TestMethod()]
    public void Day20_Part2_Input_Test()
    {
        var instance = new Day20(File.ReadAllText("inputs/day20-input.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("125", answer);
    }
}
