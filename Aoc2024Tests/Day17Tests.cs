namespace Aoc2024.Tests;

// https://adventofcode.com/2024/day/17
// --- Day 17: Chronospatial Computer ---
[TestClass()]
public class Day17Tests
{
    [TestMethod()]
    public void Part1Example2Test()
    {
        var instance = new Day17(File.ReadAllText("inputs/day17-example2.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("0,1,2", answer);
    }
    [TestMethod()]
    public void Part1Example3Test()
    {
        var instance = new Day17(File.ReadAllText("inputs/day17-example3.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("4,2,5,6,7,7,7,7,3,1,0", answer);
    }
    [TestMethod()]
    public void Part1Example6Test()
    {
        var instance = new Day17(File.ReadAllText("inputs/day17-example6.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("4,6,3,5,6,3,5,2,1,0", answer);
    }
    [TestMethod()]
    public void Part1InputTest()
    {
        var instance = new Day17(File.ReadAllText("inputs/day17-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("1,3,5,1,7,2,5,1,6", answer);
    }
    [TestMethod()]
    public void Part1RedditTest()
    {
        // https://www.reddit.com/r/adventofcode/comments/1hggduo/2024_day_17_part_2_a_challenging_test_case/
        var instance = new Day17(File.ReadAllText("inputs/day17-reddit.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("6,0,4,5,4,5,2,0", answer);
    }

    [TestMethod()]
    public void Part2Example7Test()
    {
        var instance = new Day17(File.ReadAllText("inputs/day17-example7.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("117440", answer);
    }
    [TestMethod()]
    public void Part2InputTest()
    {
        var instance = new Day17(File.ReadAllText("inputs/day17-input.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("236555997372013", answer);
    }
    [TestMethod()]
    public void Part2RedditTest()
    {
        // https://www.reddit.com/r/adventofcode/comments/1hggduo/2024_day_17_part_2_a_challenging_test_case/
        var instance = new Day17(File.ReadAllText("inputs/day17-reddit.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("202797954918051", answer);
    }
}