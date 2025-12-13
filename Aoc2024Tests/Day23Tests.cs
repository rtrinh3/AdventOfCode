namespace Aoc2024.Tests;

// https://adventofcode.com/2024/day/23
// --- Day 23: LAN Party ---
[TestClass()]
public class Day23Tests
{
    [TestMethod()]
    public void Part1ExampleTest()
    {
        var instance = new Day23(File.ReadAllText("inputs/day23-example.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("7", answer);
    }
    [TestMethod()]
    public void Part1InputTest()
    {
        var instance = new Day23(File.ReadAllText("inputs/day23-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("1108", answer);
    }

    [TestMethod()]
    public void Part2ExampleTest()
    {
        var instance = new Day23(File.ReadAllText("inputs/day23-example.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("co,de,ka,ta", answer);
    }
    [TestMethod()]
    public void Part2InputTest()
    {
        var instance = new Day23(File.ReadAllText("inputs/day23-input.txt"));
        var answer = instance.Part2();
        Assert.AreEqual("ab,cp,ep,fj,fl,ij,in,ng,pl,qr,rx,va,vf", answer);
    }
}
