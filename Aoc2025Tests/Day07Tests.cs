namespace Aoc2025.Tests;

[TestClass()]
public class Day07Tests
{
    [TestMethod()]
    public void Day07_Part1_Example_Test()
    {
        var instance = new Day07(File.ReadAllText("day07-example.txt"));
        var answer = instance.Part1();
        Assert.Inconclusive(answer);
        // Assert.AreEqual("EXPECTED_ANSWER", answer);
    }

    [TestMethod()]
    public void Day07_Part1_Input_Test()
    {
        var instance = new Day07(File.ReadAllText("day07-input.txt"));
        var answer = instance.Part1();
        Assert.Inconclusive(answer);
        // Assert.AreEqual("EXPECTED_ANSWER", answer);
    }

    [TestMethod()]
    public void Day07_Part2_Example_Test()
    {
        var instance = new Day07(File.ReadAllText("day07-example.txt"));
        var answer = instance.Part2();
        Assert.Inconclusive(answer);
        // Assert.AreEqual("EXPECTED_ANSWER", answer);
    }

    [TestMethod()]
    public void Day07_Part2_Input_Test()
    {
        var instance = new Day07(File.ReadAllText("day07-input.txt"));
        var answer = instance.Part2();
        Assert.Inconclusive(answer);
        // Assert.AreEqual("EXPECTED_ANSWER", answer);
    }
}