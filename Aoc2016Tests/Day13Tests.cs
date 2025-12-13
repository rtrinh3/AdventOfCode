namespace Aoc2016.Tests;

// https://adventofcode.com/2016/day/13
// --- Day 13: A Maze of Twisty Little Cubicles ---
[TestClass()]
public class Day13Tests
{
    [TestMethod()]
    public void Day13_Part1_Example_Test()
    {
        var seed = File.ReadAllText("inputs/day13-example.txt");
        var instance = new Day13(int.Parse(seed), 7, 4);
        var answer = instance.Part1();
        Assert.AreEqual("11", answer);
    }
    [TestMethod()]
    public void Day13_Part1_Input_Test()
    {
        var instance = new Day13(File.ReadAllText("inputs/day13-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("90", answer);
    }

    [TestMethod()]
    public void Day13_Part2_Input_Test()
    {
        var instance = new Day13(File.ReadAllText("inputs/day13-input.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("135", answer);
    }
}
