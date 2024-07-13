using System.Text.RegularExpressions;

namespace Aoc2020
{
    // https://adventofcode.com/2020/day/4
    // --- Day 4: Passport Processing ---
    public class Day04(string input) : IAocDay
    {
        private readonly string[] passports = input.TrimEnd().ReplaceLineEndings("\n").Split("\n\n");

        public string Part1()
        {
            string[] REQUIRED_FIELDS = ["byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"];
            bool isPassportValid(string p) => REQUIRED_FIELDS.All(f => p.Contains(f));
            int count = passports.Count(isPassportValid);
            return count.ToString();
        }

        public string Part2()
        {
            int count = 0;
            foreach (string p in passports)
            {
                var fields = Regex.Matches(p, @"(\S+):(\S+)");
                uint checks = 0U;
                foreach (var f in fields.AsEnumerable())
                {
                    string key = f.Groups[1].Value;
                    string value = f.Groups[2].Value;
                    switch (key)
                    {
                        case "byr":
                            if (Regex.IsMatch(value, @"^\d{4}$") && int.TryParse(value, out int byr) && 1920 <= byr && byr <= 2002)
                            {
                                checks |= 1 << 0;
                            }
                            break;
                        case "iyr":
                            if (Regex.IsMatch(value, @"^\d{4}$") && int.TryParse(value, out int iyr) && 2010 <= iyr && iyr <= 2020)
                            {
                                checks |= 1 << 1;
                            }
                            break;
                        case "eyr":
                            if (Regex.IsMatch(value, @"^\d{4}$") && int.TryParse(value, out int eyr) && 2020 <= eyr && eyr <= 2030)
                            {
                                checks |= 1 << 2;
                            }
                            break;
                        case "hgt":
                            var hgt = Regex.Match(value, @"^(\d+)(cm|in)$");
                            if (hgt.Success)
                            {
                                int num = int.Parse(hgt.Groups[1].Value);
                                string unit = hgt.Groups[2].Value;
                                if ((unit == "cm" && 150 <= num && num <= 193) || (unit == "in" && 59 <= num && num <= 76))
                                {
                                    checks |= 1 << 3;
                                }
                            }
                            break;
                        case "hcl":
                            if (Regex.IsMatch(value, @"^#[0-9a-f]{6}$"))
                            {
                                checks |= 1 << 4;
                            }
                            break;
                        case "ecl":
                            if (Regex.IsMatch(value, @"^(amb|blu|brn|gry|grn|hzl|oth)$"))
                            {
                                checks |= 1 << 5;
                            }
                            break;
                        case "pid":
                            if (Regex.IsMatch(value, @"^\d{9}$"))
                            {
                                checks |= 1 << 6;
                            }
                            break;
                    }
                }
                if (checks == 0x7fu)
                {
                    count++;
                }
            }
            return count.ToString();
        }
    }
}
