﻿namespace Aoc2024.Tests;

// https://adventofcode.com/2024/day/20
// --- Day 20: Race Condition ---
[TestClass()]
public class Day20Tests
{
    [TestMethod()]
    public void Part1ExampleTest()
    {
        var instance = new Day20(File.ReadAllText("day20-example.txt"));
        Dictionary<int, int> expected = new()
        {
            [2] = 14,
            [4] = 14,
            [6] = 2,
            [8] = 4,
            [10] = 2,
            [12] = 3,
            [20] = 1,
            [36] = 1,
            [38] = 1,
            [40] = 1,
            [64] = 1
        };
        var cheats = instance.FindCheats(2);
        var cheatGroups = cheats.GroupBy(c => c.Saved).ToDictionary(g => g.Key, g => g.Count());
        foreach (var x in expected)
        {
            Assert.AreEqual(expected[x.Key], cheatGroups[x.Key]);
        }
    }
    [TestMethod()]
    public void Part1InputTest()
    {
        var instance = new Day20(File.ReadAllText("day20-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("1197", answer);
    }

    [TestMethod()]
    public void Part2ExampleTest()
    {
        var instance = new Day20(File.ReadAllText("day20-example.txt"));
        Dictionary<int, int> expected = new()
        {
            [50] = 32,
            [52] = 31,
            [54] = 29,
            [56] = 39,
            [58] = 25,
            [60] = 23,
            [62] = 20,
            [64] = 19,
            [66] = 12,
            [68] = 14,
            [70] = 12,
            [72] = 22,
            [74] = 4,
            [76] = 3,
        };
        var cheats = instance.FindCheats(20);
        var cheatGroups = cheats.GroupBy(c => c.Saved).ToDictionary(g => g.Key, g => g.Count());
        foreach (var x in expected)
        {
            Assert.AreEqual(expected[x.Key], cheatGroups[x.Key]);
        }
    }
    [TestMethod()]
    public void Part2InputTest()
    {
        var instance = new Day20(File.ReadAllText("day20-input.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("944910", answer);
    }
    [TestMethod()]
    public void Part2RedditTest()
    {
        // https://www.reddit.com/r/adventofcode/comments/1hih8yx/2024_day_20_part_3_your_code_is_too_general_lets/
        var instance = new Day20(File.ReadAllText("day20-reddit.txt"));
        Dictionary<int, int> expected = new()
        {
            [28] = 436, // https://www.reddit.com/r/adventofcode/comments/1hih8yx/comment/m2z6igc/
            [30] = 66, // https://www.reddit.com/r/adventofcode/comments/1hih8yx/comment/m2yroqw/
            [32] = 167,
            [34] = 19,
            [36] = 39,
            [38] = 2,
            [40] = 6,
        };
        var cheats = instance.FindCheats(20);
        Assert.AreEqual(299, cheats.Count(x => x.Saved >= 30));
        var cheatGroups = cheats.GroupBy(c => c.Saved).ToDictionary(g => g.Key, g => g.Count());
        foreach (var x in expected)
        {
            Assert.AreEqual(expected[x.Key], cheatGroups[x.Key]);
        }
    }
}