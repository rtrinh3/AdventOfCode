namespace Aoc2016.Tests;

// https://adventofcode.com/2016/day/21
// --- Day 21: Scrambled Letters and Hash ---
[TestClass()]
public class Day21Tests
{
    [TestMethod()]
    public void Day21_Part1_Example_Test()
    {
        var instance = new Day21(File.ReadAllText("inputs/day21-example.txt"));
        var answer = instance.Scramble("abcde");
        Assert.AreEqual("decab", answer);
    }
    [TestMethod()]
    public void Day21_Part1_Input_Test()
    {
        var instance = new Day21(File.ReadAllText("inputs/day21-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("hcdefbag", answer);
    }

    [TestMethod()]
    public void Day21_Part2_Input_Test()
    {
        var instance = new Day21(File.ReadAllText("inputs/day21-input.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("fbhaegdc", answer);
    }
}
