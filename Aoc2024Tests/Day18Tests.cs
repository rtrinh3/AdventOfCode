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

    //[TestMethod()]
    //public void Part2ExampleTest()
    //{
    //    var instance = new Day18(File.ReadAllText("day18-example.txt"));
    //    var answer = instance.Part2();
    //    Assert.AreEqual("81", answer);
    //}
    //[TestMethod()]
    //public void Part2InputTest()
    //{
    //    var instance = new Day18(File.ReadAllText("day18-input.txt"));
    //    var answer = instance.Part2();
    //    Assert.AreEqual("928", answer);
    //}
}