namespace Aoc2025.Tests;

[TestClass()]
public class Day02Tests
{
    [TestMethod()]
    public void Day02_Part1_Example_Test()
    {
        var instance = new Day02(File.ReadAllText("day02-example.txt"));
        var answer = instance.Part1();
        Assert.Inconclusive(answer);
        // Assert.AreEqual("EXPECTED_ANSWER", answer);
    }

    [TestMethod()]
    public void Day02_Part1_Input_Test()
    {
        var instance = new Day02(File.ReadAllText("day02-input.txt"));
        var answer = instance.Part1();
        Assert.Inconclusive(answer);
        // Assert.AreEqual("EXPECTED_ANSWER", answer);
    }

    [TestMethod()]
    public void Day02_Part2_Example_Test()
    {
        var instance = new Day02(File.ReadAllText("day02-example.txt"));
        var answer = instance.Part2();
        Assert.Inconclusive(answer);
        // Assert.AreEqual("EXPECTED_ANSWER", answer);
    }

    [TestMethod()]
    public void Day02_Part2_Input_Test()
    {
        var instance = new Day02(File.ReadAllText("day02-input.txt"));
        var answer = instance.Part2();
        Assert.Inconclusive(answer);
        // Assert.AreEqual("EXPECTED_ANSWER", answer);
    }
}