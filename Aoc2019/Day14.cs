using System.Text.RegularExpressions;

namespace Aoc2019
{
    // https://adventofcode.com/2019/day/14
    public class Day14 : IAocDay
    {
        private record Reaction(int ProductQuantity, Dictionary<string, int> Reactants);

        readonly Dictionary<string, Reaction> reactions = new();

        public Day14(string input)
        {
            foreach (string line in input.TrimEnd().Split('\n'))
            {
                Dictionary<string, int> reactants = new Dictionary<string, int>();
                var matches = Regex.Matches(line, @"(\d+) ([a-zA-Z]+)").ToArray();
                foreach (var match in matches[0..^1])
                {
                    reactants[match.Groups[2].Value] = int.Parse(match.Groups[1].Value);
                }
                var product = matches[^1];
                reactions[product.Groups[2].Value] = new Reaction(int.Parse(product.Groups[1].Value), reactants);
            }
        }

        public string Part1()
        {
            return DoPart1().ToString();
        }

        private long DoPart1()
        {
            var partOneInventory = FindOreForFuel(1);
            var partOneOre = -partOneInventory["ORE"];
            return partOneOre;
        }

        public string Part2()
        {
            const long TARGET_ORE = 1000000000000L;
            long lowerBound = 0L;
            long? upperBound = null;
            long candidate = TARGET_ORE / DoPart1();
            while (upperBound == null || lowerBound < upperBound)
            {
                //Console.WriteLine($"Trying {candidate} fuel");
                var inventory = FindOreForFuel(candidate);
                var oreRequired = -inventory["ORE"];
                if (oreRequired <= TARGET_ORE)
                {
                    lowerBound = candidate;
                    if (upperBound == null)
                    {
                        candidate *= 2;
                    }
                    else
                    {
                        candidate = upperBound.Value - (upperBound.Value - lowerBound) / 2;
                    }
                }
                else
                {
                    upperBound = candidate - 1;
                    candidate = upperBound.Value - (upperBound.Value - lowerBound) / 2;
                }
                //Console.WriteLine($"Needed {oreRequired} ore");
            }
            //Console.WriteLine($"You can get {candidate} fuel");
            return candidate.ToString();
        }

        Dictionary<string, long> FindOreForFuel(long fuelWanted)
        {
            Dictionary<string, long> inventory = new();
            inventory["FUEL"] = -fuelWanted;
            while (inventory.Where(kvp => kvp.Key != "ORE").Any(kvp => kvp.Value < 0))
            {
                var missingIngredients = inventory.Where(kvp => kvp.Key != "ORE" && kvp.Value < 0).Select(kvp => kvp.Key).ToList();
                foreach (var missing in missingIngredients)
                {
                    var reaction = reactions[missing];
                    var reactionCopies = (long)Math.Ceiling(-inventory[missing] / (decimal)reaction.ProductQuantity);
                    foreach (var ingredient in reaction.Reactants)
                    {
                        inventory[ingredient.Key] = inventory.GetValueOrDefault(ingredient.Key) - reactionCopies * ingredient.Value;
                    }
                    inventory[missing] += reactionCopies * reaction.ProductQuantity;
                }
            }
            return inventory;
        }
    }
}
