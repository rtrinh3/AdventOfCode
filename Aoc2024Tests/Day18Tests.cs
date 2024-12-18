namespace Aoc2024.Tests;

[TestClass()]
public class Day18Tests
{
    [TestMethod()]
    public void Part1ExampleTest()
    {
        var instance = new Day18(File.ReadAllText("day18-example.txt"));
        var answer = instance.DoPart1(6, 6, 12);
        Assert.AreEqual(22, answer);
    }
    [TestMethod()]
    public void Part1InputTest()
    {
        var instance = new Day18(File.ReadAllText("day18-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("284", answer);
    }

    [TestMethod()]
    public void Part2ExampleTest()
    {
        var instance = new Day18(File.ReadAllText("day18-example.txt"));
        var answer = instance.DoPart2(6, 6);
        Assert.AreEqual((6, 1), answer);
    }
    [TestMethod()]
    public void Part2InputTest()
    {
        var instance = new Day18(File.ReadAllText("day18-input.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("51,50", answer);
    }
    [TestMethod()]
    public void Part2RedditTest()
    {
        // https://www.reddit.com/r/adventofcode/comments/1hgy6nb/2024_day_18_can_you_solve_it_in_linear_time/
        var instance = new Day18(File.ReadAllText("day18-reddit.txt"));
        var answer = instance.DoPart2(213 - 1, 213 - 1);
        Assert.AreEqual((200, 208), answer);
    }
}