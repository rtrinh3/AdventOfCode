namespace Aoc2024.Tests;

// https://adventofcode.com/2024/day/24
// --- Day 24: Crossed Wires ---
[TestClass()]
public class Day24Tests
{
    [TestMethod()]
    public void Part1Example1Test()
    {
        var instance = new Day24(File.ReadAllText("day24-example1.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("4", answer);
    }
    [TestMethod()]
    public void Part1Example2Test()
    {
        var instance = new Day24(File.ReadAllText("day24-example2.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("2024", answer);
    }
    [TestMethod()]
    public void Part1InputTest()
    {
        var instance = new Day24(File.ReadAllText("day24-input.txt"));
        var answer = instance.Part1();
        Assert.AreEqual("59364044286798", answer);
    }

    [TestMethod()]
    public void Part2Example3Test()
    {
        var instance = new Day24(File.ReadAllText("day24-example3.txt"));
        static int EvaluateErrorBitwiseAnd(Func<ulong, ulong, ulong> system)
        {
            int errors = 0;
            for (ulong i = 0; i < (1 << 6); i++)
            {
                for (ulong j = 0; j < (1 << 6); j++)
                {
                    ulong expected = i & j;
                    ulong actual = system(i, j);
                    if (expected!= actual)
                    {
                        errors++;
                    }
                }
            }
            return errors;
        }
        var answer = instance.DoPart2(2, EvaluateErrorBitwiseAnd);        
        Assert.AreEqual("z00,z01,z02,z05", answer);
    }
    [TestMethod(), Timeout(60_000)]
    public void Part2InputTest()
    {
        var instance = new Day24(File.ReadAllText("day24-input.txt"));
        var answer = instance.Part2();
        // (gmt,z07), (qjj,cbj), (dmn, z18), (cfk,z35)
        Assert.AreEqual("cbj,cfk,dmn,gmt,qjj,z07,z18,z35", answer);
    }
}
