namespace Aoc2016.Tests;

// https://adventofcode.com/2016/day/23
// --- Day 23: Safe Cracking ---
[TestClass()]
public class Day23Tests
{
    [TestMethod()]
    public void Day23_Part1_Example_Test()
    {
        var instance = new Day23(File.ReadAllText("day23-example.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("3", answer);
    }
    [TestMethod()]
    public void Day23_Part1_Input_Test()
    {
        var instance = new Day23(File.ReadAllText("day23-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("13050", answer);
    }

    [TestMethod(), Timeout(60_000)]
    public void Day23_Part2_Input_Test()
    {
        var instance = new Day23(File.ReadAllText("day23-input.txt"));
        var answer = instance.Part2();
        //Assert.AreEqual("13050", answer);
        Assert.Inconclusive(answer);
    }
}
