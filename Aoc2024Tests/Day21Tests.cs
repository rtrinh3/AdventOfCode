namespace Aoc2024.Tests;

// https://adventofcode.com/2024/day/21
// --- Day 21: Keypad Conundrum ---
[TestClass()]
public class Day21Tests
{
    [TestMethod()]
    public void Part1ExampleTest()
    {
        var file = File.ReadAllText("day21-example.txt");
        var totalInstance = new Day21(file);
        var totalAnswer = totalInstance.Part1();
        Assert.AreEqual("126384", totalAnswer);

        var lines = file.TrimEnd().ReplaceLineEndings("\n").Split('\n');
        int[] expected = [68 * 29, 60 * 980, 68 * 179, 64 * 456, 64 * 379];
        Assert.AreEqual(expected.Length, lines.Length);
        for (int i = 0; i < expected.Length; i++)
        {
            var lineInstance = new Day21(lines[i]);
            var lineAnswer = lineInstance.Part1();
            Assert.AreEqual(expected[i].ToString(), lineAnswer);
        }
    }
    [TestMethod()]
    public void Part1InputTest()
    {
        var instance = new Day21(File.ReadAllText("day21-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("132532", answer);
    }

    [TestMethod()]
    public void Part2InputTest()
    {
        var instance = new Day21(File.ReadAllText("day21-input.txt"));
        var answer = instance.Part2();
        //Assert.AreEqual("132532", answer);
        Assert.Inconclusive(answer);
    }
}