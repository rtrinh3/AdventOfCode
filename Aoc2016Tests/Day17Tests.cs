namespace Aoc2016.Tests;

// https://adventofcode.com/2016/day/17
// --- Day 17: Two Steps Forward ---
[TestClass()]
public class Day17Tests
{
    [TestMethod()]
    public void Day17_Part1_Example1_Test()
    {
        var instance = new Day17(File.ReadAllText("inputs/day17-example1.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("DDRRRD", answer);
    }
    [TestMethod()]
    public void Day17_Part1_Example2_Test()
    {
        var instance = new Day17(File.ReadAllText("inputs/day17-example2.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("DDUDRLRRUDRD", answer);
    }
    [TestMethod()]
    public void Day17_Part1_Example3_Test()
    {
        var instance = new Day17(File.ReadAllText("inputs/day17-example3.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("DRURDRUDDLLDLUURRDULRLDUUDDDRR", answer);
    }
    [TestMethod()]
    public void Day17_Part1_Input_Test()
    {
        var instance = new Day17(File.ReadAllText("inputs/day17-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("RRRLDRDUDD", answer);
    }

    [TestMethod()]
    public void Day17_Part2_Example1_Test()
    {
        var instance = new Day17(File.ReadAllText("inputs/day17-example1.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("370", answer);
    }
    [TestMethod()]
    public void Day17_Part2_Example2_Test()
    {
        var instance = new Day17(File.ReadAllText("inputs/day17-example2.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("492", answer);
    }
    [TestMethod()]
    public void Day17_Part2_Example3_Test()
    {
        var instance = new Day17(File.ReadAllText("inputs/day17-example3.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("830", answer);
    }
    [TestMethod()]
    public void Day17_Part2_Input_Test()
    {
        var instance = new Day17(File.ReadAllText("inputs/day17-input.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("706", answer);
    }
}