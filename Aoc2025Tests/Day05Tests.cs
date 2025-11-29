namespace Aoc2025.Tests;

[TestClass()]
public class Day05Tests
{
    [TestMethod()]
    public void Day05_Part1_Example_Test()
    {
        var instance = new Day05(File.ReadAllText("day05-example.txt"));
        var answer = instance.Part1();
        Assert.Inconclusive(answer);
        // Assert.AreEqual("EXPECTED_ANSWER", answer);
    }

    [TestMethod()]
    public void Day05_Part1_Input_Test()
    {
        var instance = new Day05(File.ReadAllText("day05-input.txt"));
        var answer = instance.Part1();
        Assert.Inconclusive(answer);
        // Assert.AreEqual("EXPECTED_ANSWER", answer);
    }

    [TestMethod()]
    public void Day05_Part2_Example_Test()
    {
        var instance = new Day05(File.ReadAllText("day05-example.txt"));
        var answer = instance.Part2();
        Assert.Inconclusive(answer);
        // Assert.AreEqual("EXPECTED_ANSWER", answer);
    }

    [TestMethod()]
    public void Day05_Part2_Input_Test()
    {
        var instance = new Day05(File.ReadAllText("day05-input.txt"));
        var answer = instance.Part2();
        Assert.Inconclusive(answer);
        // Assert.AreEqual("EXPECTED_ANSWER", answer);
    }
}