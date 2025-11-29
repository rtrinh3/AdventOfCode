namespace Aoc2025.Tests;

[TestClass()]
public class Day06Tests
{
    [TestMethod()]
    public void Day06_Part1_Example_Test()
    {
        var instance = new Day06(File.ReadAllText("day06-example.txt"));
        var answer = instance.Part1();
        Assert.Inconclusive(answer);
        // Assert.AreEqual("EXPECTED_ANSWER", answer);
    }

    [TestMethod()]
    public void Day06_Part1_Input_Test()
    {
        var instance = new Day06(File.ReadAllText("day06-input.txt"));
        var answer = instance.Part1();
        Assert.Inconclusive(answer);
        // Assert.AreEqual("EXPECTED_ANSWER", answer);
    }

    [TestMethod()]
    public void Day06_Part2_Example_Test()
    {
        var instance = new Day06(File.ReadAllText("day06-example.txt"));
        var answer = instance.Part2();
        Assert.Inconclusive(answer);
        // Assert.AreEqual("EXPECTED_ANSWER", answer);
    }

    [TestMethod()]
    public void Day06_Part2_Input_Test()
    {
        var instance = new Day06(File.ReadAllText("day06-input.txt"));
        var answer = instance.Part2();
        Assert.Inconclusive(answer);
        // Assert.AreEqual("EXPECTED_ANSWER", answer);
    }
}