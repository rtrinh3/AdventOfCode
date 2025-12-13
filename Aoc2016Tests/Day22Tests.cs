namespace Aoc2016.Tests;

// https://adventofcode.com/2016/day/22
// --- Day 22: Grid Computing ---
[TestClass()]
public class Day22Tests
{
    [TestMethod()]
    public void Day22_Part1_Input_Test()
    {
        var instance = new Day22(File.ReadAllText("inputs/day22-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("993", answer);
    }

    [TestMethod()]
    public void Day22_Part2_Example_Test()
    {
        var instance = new Day22(File.ReadAllText("inputs/day22-example.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("7", answer);
    }
    [TestMethod(), Timeout(60_000)]
    public void Day22_Part2_Input_Test()
    {
        var instance = new Day22(File.ReadAllText("inputs/day22-input.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("202", answer);
    }
}
