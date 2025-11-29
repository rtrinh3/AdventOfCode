namespace Aoc2025.Tests;

[TestClass()]
public class Day03Tests
{
    [TestMethod()]
    public void Day03_Part1_Example_Test()
    {
        var instance = new Day03(File.ReadAllText("day03-example.txt"));
        var answer = instance.Part1();
        Assert.Inconclusive(answer);
        // Assert.AreEqual("EXPECTED_ANSWER", answer);
    }

    [TestMethod()]
    public void Day03_Part1_Input_Test()
    {
        var instance = new Day03(File.ReadAllText("day03-input.txt"));
        var answer = instance.Part1();
        Assert.Inconclusive(answer);
        // Assert.AreEqual("EXPECTED_ANSWER", answer);
    }

    [TestMethod()]
    public void Day03_Part2_Example_Test()
    {
        var instance = new Day03(File.ReadAllText("day03-example.txt"));
        var answer = instance.Part2();
        Assert.Inconclusive(answer);
        // Assert.AreEqual("EXPECTED_ANSWER", answer);
    }

    [TestMethod()]
    public void Day03_Part2_Input_Test()
    {
        var instance = new Day03(File.ReadAllText("day03-input.txt"));
        var answer = instance.Part2();
        Assert.Inconclusive(answer);
        // Assert.AreEqual("EXPECTED_ANSWER", answer);
    }
}