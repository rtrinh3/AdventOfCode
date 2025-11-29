namespace Aoc2025.Tests;

[TestClass()]
public class Day08Tests
{
    [TestMethod()]
    public void Day08_Part1_Example_Test()
    {
        var instance = new Day08(File.ReadAllText("day08-example.txt"));
        var answer = instance.Part1();
        Assert.Inconclusive(answer);
        // Assert.AreEqual("EXPECTED_ANSWER", answer);
    }

    [TestMethod()]
    public void Day08_Part1_Input_Test()
    {
        var instance = new Day08(File.ReadAllText("day08-input.txt"));
        var answer = instance.Part1();
        Assert.Inconclusive(answer);
        // Assert.AreEqual("EXPECTED_ANSWER", answer);
    }

    [TestMethod()]
    public void Day08_Part2_Example_Test()
    {
        var instance = new Day08(File.ReadAllText("day08-example.txt"));
        var answer = instance.Part2();
        Assert.Inconclusive(answer);
        // Assert.AreEqual("EXPECTED_ANSWER", answer);
    }

    [TestMethod()]
    public void Day08_Part2_Input_Test()
    {
        var instance = new Day08(File.ReadAllText("day08-input.txt"));
        var answer = instance.Part2();
        Assert.Inconclusive(answer);
        // Assert.AreEqual("EXPECTED_ANSWER", answer);
    }
}