namespace Aoc2016.Tests;

// https://adventofcode.com/2016/day/16
// --- Day 16: Dragon Checksum ---
[TestClass()]
public class Day16Tests
{
    [TestMethod()]
    public void Day16_Part1_Example_Test()
    {
        var instance = new Day16(File.ReadAllText("day16-example.txt"));
        var answer = instance.DoPuzzle(20);
        Assert.AreEqual("01100", answer);
    }
    [TestMethod()]
    public void Day16_Part1_Input_Test()
    {
        var instance = new Day16(File.ReadAllText("day16-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("10111110010110110", answer);
    }

    [TestMethod()]
    public void Day16_Part2_Input_Test()
    {
        var instance = new Day16(File.ReadAllText("day16-input.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("01101100001100100", answer);
    }
}
