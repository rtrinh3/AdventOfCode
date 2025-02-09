namespace Aoc2015.Tests;

// https://adventofcode.com/2015/day/21
// --- Day 21: RPG Simulator 20XX ---
[TestClass()]
public class Day21Tests
{
    [TestMethod()]
    public void Part1ExampleTest()
    {
        var player = new Day21.Combatant(8, 5, 5);
        var boss = new Day21.Combatant(12, 7, 2);
        var fight = Day21.SimulateCombat(player, boss);
        Assert.AreEqual(0, fight);
    }

    [TestMethod()]
    public void Part1InputTest()
    {
        var instance = new Day21(File.ReadAllText("day21-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("91", answer);
    }

    [TestMethod()]
    public void Part2InputTest()
    {
        var instance = new Day21(File.ReadAllText("day21-input.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("158", answer);
    }
}
