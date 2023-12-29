using AocCommon;
using System.Numerics;

namespace Aoc2019
{
    // https://adventofcode.com/2019/day/17
    public class Day17(string input) : IAocDay
    {
        readonly IntcodeInterpreter interpreter = new(input);

        public string Part1()
        {
            interpreter.Reset();
            var partOneOutput = interpreter.RunToEnd();
            var partOneOutputString = string.Join("", partOneOutput.Select(n => (char)n));
            //Console.WriteLine(partOneOutputString);
            int alignment = 0;
            string[] scaffoldMatrix = partOneOutputString.Split('\n').Where(s => s.Length > 0).ToArray();
            for (int row = 1; row < scaffoldMatrix.Length - 1; row++)
            {
                for (int col = 1; col < scaffoldMatrix[row].Length - 1; col++)
                {
                    if (scaffoldMatrix[row][col] == '#' && scaffoldMatrix[row + 1][col] == '#' && scaffoldMatrix[row - 1][col] == '#' && scaffoldMatrix[row][col + 1] == '#' && scaffoldMatrix[row][col - 1] == '#')
                    {
                        alignment += row * col;
                    }
                }
            }
            //CalculatePath(scaffoldMatrix);
            return alignment.ToString();
        }

        private static List<object> CalculatePath(string[] scaffoldMatrix)
        {
            // Calculate full route
            int startRow = 0;
            int startCol = 0;
            for (startRow = 0; startRow < scaffoldMatrix.Length; startRow++)
            {
                for (startCol = 0; startCol < scaffoldMatrix[startRow].Length; startCol++)
                {
                    if (scaffoldMatrix[startRow][startCol] == '^')
                    {
                        goto startFound;
                    }
                }
            }
        startFound:;
            char GetScaffoldMatrix(VectorRC coord)
            {
                if (coord.Row < 0 || coord.Row >= scaffoldMatrix.Length)
                {
                    return '\0';
                }
                var matrixRow = scaffoldMatrix[coord.Row];
                if (coord.Col < 0 || coord.Col >= matrixRow.Length)
                {
                    return '\0';
                }
                return matrixRow[coord.Col];
            }
            var start = new VectorRC(startRow, startCol);
            var position = start;
            var orientation = VectorRC.Up;
            List<object> cmds = new();
            int advanceStreak = 0;
            while (true)
            {
                var front = position + orientation;
                char peekFront = GetScaffoldMatrix(front);
                if (peekFront == '#')
                {
                    advanceStreak++;
                    position = front;
                    continue;
                }
                var turnLeft = orientation.RotatedLeft();
                char peekLeft = GetScaffoldMatrix(position + turnLeft);
                if (peekLeft == '#')
                {
                    if (advanceStreak > 0)
                    {
                        cmds.Add(advanceStreak);
                        advanceStreak = 0;
                    }
                    cmds.Add("L");
                    orientation = turnLeft;
                    continue;
                }
                var turnRight = orientation.RotatedRight();
                char peekRight = GetScaffoldMatrix(position + turnRight);
                if (peekRight == '#')
                {
                    if (advanceStreak > 0)
                    {
                        cmds.Add(advanceStreak);
                        advanceStreak = 0;
                    }
                    cmds.Add("R");
                    orientation = turnRight;
                    continue;
                }
                break;
            }
            cmds.Add(advanceStreak);
            Console.WriteLine(string.Join(",", cmds));
            return cmds;
        }

        public string Part2()
        {
            // Run solution
            interpreter.Reset();
            interpreter.Poke(0, 2);
            string partTwoScript = @"A,C,C,B,A,C,B,A,C,B
L,6,R,12,L,4,L,6
L,6,L,10,L,10,R,6
R,6,L,6,R,12
n
".ReplaceLineEndings("\n");
            var partTwoOutput = interpreter.RunToEnd(partTwoScript.Select(c => (BigInteger)c)).ToList();
            //foreach (BigInteger output in partTwoOutput)
            //{
            //    if (output <= 0xFF)
            //    {
            //        Console.Write((char)output);
            //    }
            //    else
            //    {
            //        Console.WriteLine(output);
            //    }
            //}
            return partTwoOutput.Last().ToString();
        }
    }
}
