namespace Aoc2025.Tests;

[TestClass()]
public class Day04Tests
{
    [TestMethod()]
    public void Day04_Part1_Example_Test()
    {
        var instance = new Day04(File.ReadAllText("day04-example.txt"));
        var answer = instance.Part1();
        Assert.Inconclusive(answer);
        // Assert.AreEqual("EXPECTED_ANSWER", answer);
    }

    [TestMethod()]
    public void Day04_Part1_Input_Test()
    {
        var instance = new Day04(File.ReadAllText("day04-input.txt"));
        var answer = instance.Part1();
        Assert.Inconclusive(answer);
        // Assert.AreEqual("EXPECTED_ANSWER", answer);
    }

    [TestMethod()]
    public void Day04_Part2_Example_Test()
    {
        var instance = new Day04(File.ReadAllText("day04-example.txt"));
        var answer = instance.Part2();
        Assert.Inconclusive(answer);
        // Assert.AreEqual("EXPECTED_ANSWER", answer);
    }

    [TestMethod()]
    public void Day04_Part2_Input_Test()
    {
        var instance = new Day04(File.ReadAllText("day04-input.txt"));
        var answer = instance.Part2();
        Assert.Inconclusive(answer);
        // Assert.AreEqual("EXPECTED_ANSWER", answer);
    }
}