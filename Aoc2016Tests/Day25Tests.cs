namespace Aoc2016.Tests;

// https://adventofcode.com/2016/day/25
// --- Day 25: Clock Signal ---
[TestClass()]
public class Day25Tests
{
    [TestMethod(), Timeout(60_000)]
    public void Day25_Part1_Input_Test()
    {
        var instance = new Day25(File.ReadAllText("inputs/day25-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("196", answer);
    }
}
