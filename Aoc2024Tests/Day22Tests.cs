namespace Aoc2024.Tests;

// https://adventofcode.com/2024/day/22
// --- Day 22: Monkey Market ---
[TestClass()]
public class Day22Tests
{

    [TestMethod()]
    public void Part1ExampleTest()
    {
        var file = File.ReadAllText("day22-example1.txt");
        var totalInstance = new Day22(file);
        var totalAnswer = totalInstance.Part1();
        Assert.AreEqual("37327623", totalAnswer);

        var lines = file.TrimEnd().ReplaceLineEndings("\n").Split('\n');
        int[] expected = [8685429, 4700978, 15273692, 8667524];
        Assert.AreEqual(expected.Length, lines.Length);
        for (int i = 0; i < expected.Length; i++)
        {
            var lineInstance = new Day22(lines[i]);
            var lineAnswer = lineInstance.Part1();
            Assert.AreEqual(expected[i].ToString(), lineAnswer);
        }
    }
    [TestMethod()]
    public void Part1InputTest()
    {
        var instance = new Day22(File.ReadAllText("day22-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("13185239446", answer);
    }

    [TestMethod()]
    public void Part2ExampleTest()
    {
        var instance = new Day22(File.ReadAllText("day22-example2.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("23", answer);
    }
    [TestMethod()]
    public void Part2InputTest()
    {
        var instance = new Day22(File.ReadAllText("day22-input.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("1501", answer);
    }
}