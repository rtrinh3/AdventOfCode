using AocCommon;

namespace Aoc2020
{
    // https://adventofcode.com/2020/day/21
    // --- Day 21: Allergen Assessment ---
    public class Day21 : IAocDay
    {
        private readonly List<(HashSet<string> Ingredients, HashSet<string> Allergens)> Foods;
        private readonly HashSet<string> AllIngredients;
        private readonly HashSet<string> AllAllergens;
        private readonly Func<string, string, bool> IsIngredientCompatibleWithAllergen;

        public Day21(string input)
        {
            var lines = input.TrimEnd().Split('\n');
            Foods = new();
            AllIngredients = new();
            AllAllergens = new();
            foreach (var line in lines)
            {
                int parenthesisIndex = line.IndexOf('(');
                if (parenthesisIndex < 0)
                {
                    var ingredients = line.Trim().Split(' ').ToHashSet();
                    Foods.Add((ingredients, []));
                    AllIngredients.UnionWith(ingredients);
                }
                else
                {
                    var ingredients = line[0..parenthesisIndex].Trim().Split(' ').ToHashSet();
                    var allergensPart = line[parenthesisIndex..].Replace("(contains ", "").Replace(")", "");
                    var allergens = allergensPart.Split(',').Select(x => x.Trim()).ToHashSet();
                    Foods.Add((ingredients, allergens));
                    AllIngredients.UnionWith(ingredients);
                    AllAllergens.UnionWith(allergens);
                }
            }

            IsIngredientCompatibleWithAllergen = Memoization.Make((string ingredient, string allergen) =>
            {
                return Foods.All(f => !f.Allergens.Contains(allergen) || f.Ingredients.Contains(ingredient));
            });
        }

        public long Part1()
        {
            long answer = 0;
            foreach (var ingredient in AllIngredients)
            {
                if (AllAllergens.All(allergen => !IsIngredientCompatibleWithAllergen(ingredient, allergen)))
                {
                    answer += Foods.Count(f => f.Ingredients.Contains(ingredient));
                }
            }
            return answer;
        }

        public long Part2()
        {
            throw new NotImplementedException();
        }
    }
}
