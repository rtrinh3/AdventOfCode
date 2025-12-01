namespace Aoc2025.Tests;

[TestClass()]
public class Day01Tests
{
    [TestMethod()]
    public void Day01_Part1_Example_Test()
    {
        var instance = new Day01(File.ReadAllText("day01-example.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("3", answer);
    }

    [TestMethod()]
    public void Day01_Part1_Input_Test()
    {
        var instance = new Day01(File.ReadAllText("day01-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("1084", answer);
    }

    [TestMethod()]
    public void Day01_Part2_Example_Test()
    {
        var instance = new Day01(File.ReadAllText("day01-example.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("6", answer);
    }

    [TestMethod()]
    public void Day01_Part2_Input_Test()
    {
        var instance = new Day01(File.ReadAllText("day01-input.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("6475", answer);
    }
}
