namespace Aoc2024.Tests;

// https://adventofcode.com/2024/day/25
// --- Day 25: Code Chronicle ---
[TestClass()]
public class Day25Tests
{
    [TestMethod()]
    public void Part1ExampleTest()
    {
        var instance = new Day25(File.ReadAllText("day25-example.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("3", answer);
    }
    [TestMethod()]
    public void Part1InputTest()
    {
        var instance = new Day25(File.ReadAllText("day25-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("3397", answer);
    }
}
