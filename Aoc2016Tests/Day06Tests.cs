namespace Aoc2016.Tests;

// https://adventofcode.com/2016/day/6
// --- Day 6: Signals and Noise ---
[TestClass()]
public class Day06Tests
{
    [TestMethod()]
    public void Day06_Part1_Example_Test()
    {
        var instance = new Day06(File.ReadAllText("inputs/day06-example.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("easter", answer);
    }
    [TestMethod()]
    public void Day06_Part1_Input_Test()
    {
        var instance = new Day06(File.ReadAllText("inputs/day06-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("qoclwvah", answer);
    }

    [TestMethod()]
    public void Day06_Part2_Example_Test()
    {
        var instance = new Day06(File.ReadAllText("inputs/day06-example.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("advent", answer);
    }
    [TestMethod()]
    public void Day06_Part2_Input_Test()
    {
        var instance = new Day06(File.ReadAllText("inputs/day06-input.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("ryrgviuv", answer);
    }
}