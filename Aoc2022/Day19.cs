using System.Diagnostics;

namespace Aoc2022
{
    public class Day19 : IAocDay
    {
        private static readonly Dictionary<string, int> materialIndexMap = new()
        {
            ["ore"] = 0,
            ["clay"] = 1,
            ["obsidian"] = 2,
            ["geode"] = 3
        };

        private readonly int[][][] blueprints;
        private readonly int[] triangularNumbers;

        public Day19(string input)
        {
            const StringSplitOptions TrimAndNoEmpty = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;
            blueprints = input.Split("Blueprint", TrimAndNoEmpty).Select((blueprintText, blueprintIndex) =>
            {
                string[] idParts = blueprintText.Split(':', TrimAndNoEmpty);
                int id = int.Parse(idParts[0]);
                Debug.Assert(blueprintIndex + 1 == id);

                string[] robotTexts = idParts[1].Split('.', TrimAndNoEmpty);
                return robotTexts.Select((robotText, robotIndex) =>
                {
                    string[] sentenceParts = robotText.Split("costs", TrimAndNoEmpty);
                    string robotMaterial = sentenceParts[0].Split(' ', TrimAndNoEmpty)[1];
                    Debug.Assert(robotIndex == materialIndexMap[robotMaterial]);

                    string[] costTexts = sentenceParts[1].Split("and", TrimAndNoEmpty);
                    int[] costs = new int[4];
                    foreach (var cost in costTexts)
                    {
                        string[] parts = cost.Split(' ', TrimAndNoEmpty);
                        int costIndex = materialIndexMap[parts[1]];
                        int costValue = int.Parse(parts[0]);
                        costs[costIndex] = costValue;
                    }
                    return costs;
                }).ToArray();
            }).ToArray();

            triangularNumbers = new int[33];
            for (int i = 0; i < 33; i++)
            {
                triangularNumbers[i] = Enumerable.Range(0, i).Sum();
            }
        }

        public string Part1()
        {
            int answer = 0;
            Parallel.For(0, blueprints.Length, i =>
            {
                var geodeOutput = EvaluateBlueprint(i, 24);
                lock (this)
                {
                    answer += geodeOutput * (i + 1);
                }
            });
            return answer.ToString();
        }
        public string Part2()
        {
            int answer = 1;
            Parallel.For(0, Math.Min(3, blueprints.Length), i =>
            {
                var geodeOutput = EvaluateBlueprint(i, 32);
                lock (this)
                {
                    answer *= geodeOutput;
                }
            });
            return answer.ToString();
        }

        public int EvaluateBlueprint(int blueprintIndex, int timeLimit)
        {
            const int GEODE_INDEX = 3;
            var blueprint = blueprints[blueprintIndex];
            int maxGeodes = 0;
            var maxConsumptionOfMaterials = Enumerable.Range(0, 4).Select(i => blueprint.Max(r => r[i])).ToArray();
            void Visit(int minute, int[] robots, int[] materials)
            {
                if (minute == timeLimit)
                {
                    int geodes = materials[GEODE_INDEX];
                    if (geodes > maxGeodes)
                    {
                        //Console.WriteLine(geodes);
                        maxGeodes = geodes;
                    }
                    return;
                }

                // Can we theoretically catch up?
                if (materials[GEODE_INDEX] + robots[GEODE_INDEX] * (timeLimit - minute) + triangularNumbers[timeLimit - minute] <= maxGeodes)
                {
                    return;
                }

                // Wait until end
                int timeUntilEnd = timeLimit - minute;
                int[] finalMaterials = AddArrays(materials, ScaleArray(robots, timeUntilEnd));
                Visit(timeLimit, robots, finalMaterials);

                // It's the last minute, any new robot won't have any results
                if (timeUntilEnd <= 1)
                {
                    return;
                }

                // What to build next?
                for (int robotToBuildIndex = 0; robotToBuildIndex < 4; robotToBuildIndex++)
                {
                    // Is this robot worth building?
                    if (robotToBuildIndex != GEODE_INDEX && robots[robotToBuildIndex] >= maxConsumptionOfMaterials[robotToBuildIndex])
                    {
                        continue;
                    }
                    // Build the robot now?
                    var robotToBuild = blueprint[robotToBuildIndex];
                    var materialsAfterBuildingThisRobot = AddArrays(materials, ScaleArray(robotToBuild, -1));
                    if (AllPositive(materialsAfterBuildingThisRobot))
                    {
                        int[] newMaterials = AddArrays(materialsAfterBuildingThisRobot, robots);
                        int[] newRobots = [.. robots];
                        newRobots[robotToBuildIndex]++;
                        Visit(minute + 1, newRobots, newMaterials);
                    }
                    // Build it later?
                    else if (Enumerable.Range(0, 4).All(materialIndex => robotToBuild[materialIndex] == 0 || robots[materialIndex] > 0))
                    {
                        int timeUntilBuildable = Enumerable.Range(0, 4).Where(materialIndex => robotToBuild[materialIndex] != 0).Max(materialIndex => (robotToBuild[materialIndex] - materials[materialIndex] + robots[materialIndex] - 1) / robots[materialIndex]);
                        if (minute + timeUntilBuildable <= timeLimit)
                        {
                            int[] newMaterials = AddArrays(materials, ScaleArray(robots, timeUntilBuildable));
                            Visit(minute + timeUntilBuildable, robots, newMaterials);
                        }
                    }
                }
            }
            Visit(0, [1, 0, 0, 0], [0, 0, 0, 0]);
            return maxGeodes;
        }

        private static int[] AddArrays(int[] a, int[] b)
        {
            Debug.Assert(a.Length == b.Length);
            return a.Zip(b, (aa, bb) => aa + bb).ToArray();
        }
        private static int[] ScaleArray(int[] xs, int scale)
        {
            return xs.Select(x => x * scale).ToArray();
        }
        private static bool AllPositive(int[] xs)
        {
            return xs.All(x => x >= 0);
        }
    }
}
