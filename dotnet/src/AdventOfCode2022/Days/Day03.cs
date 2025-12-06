using Solutions.Base;
using Solutions.Extensions;

namespace Solutions.Days;

public class Day03: IDay<int>
{
    private List<Rucksack> ParseInput(string input)
    {
        return input.ToStringList().Select(s =>
        {
            var part1 = s.Substring(0, s.Length / 2)!.ToCharArray();
            var part2 = new HashSet<char>( s.Substring(s.Length / 2, s.Length / 2).ToCharArray());
            return new Rucksack(part1, part2);
        }).ToList();
    }

    private List<Group> ParseInput2(string input)
    {
        return input.ToStringList().Batch(3).Select(s =>
        {
            var parts = s.ToList();
            var part1 = parts[0].ToCharArray();
            var part2 = new HashSet<char>(parts[1].ToCharArray());
            var part3 = new HashSet<char>(parts[2].ToCharArray());
            return new Group(part1, part2, part3);
        }).ToList();
    }

    public int Part1(string input)
    {
        var rucksacks = ParseInput(input);
        var score = 0;

        foreach (var rucksack in rucksacks)
        {
            foreach (var item in rucksack.Part1)
            {
                if (rucksack.Part2.Contains(item))
                {
                    if (item <= 'Z')
                    {
                        score += item - 38;
                        break;
                    }

                    score += item - 96;
                    break;
                }
            }
        }

        return score;
    }

    public int Part2(string input)
    {
        var groups = ParseInput2(input);

        var score = 0;
        foreach (var group in groups)
        {
            foreach (var item in group.Rucksack1)
            {
                if (group.Rucksack2.Contains(item) && group.Rucksack3.Contains(item))
                {
                    if (item <= 'Z')
                    {
                        score += item - 38;
                        break;
                    }

                    score += item - 96;
                    break;
                }
            }
        }

        return score;
    }
}

record Group(char[] Rucksack1, HashSet<char> Rucksack2, HashSet<char> Rucksack3);

record Rucksack(char[] Part1, HashSet<char> Part2);
