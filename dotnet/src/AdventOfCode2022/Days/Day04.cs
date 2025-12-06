using Solutions.Base;
using Solutions.Extensions;

namespace Solutions.Days;

public class Day04 : IDay<int>
{
    private List<ElfGroup> ParseInput(string input)
    {
        return input.ToStringList().Select(line =>
        {
            var ranges = line.Split(",");
            var range1 = new ElfRange(
                int.Parse(ranges[0].Split("-")[0]),
                int.Parse(ranges[0].Split("-")[1]));
            var range2 = new ElfRange(
                int.Parse(ranges[1].Split("-")[0]),
                int.Parse(ranges[1].Split("-")[1]));
            return new ElfGroup(range1, range2);
        }).ToList();
    }

    private bool CheckIntersect(ElfRange range1, ElfRange range2)
    {
        return range1.From >= range2.From &&
               range1.To <= range2.To;
    }

    private bool CheckOverlap(ElfRange range1, ElfRange range2)
    {
        return range1.From <= range2.From && range1.To >= range2.From;
    }

    public int Part1(string input)
    {
        var groups = ParseInput(input);

        int count = 0;
        foreach (var group in groups)
        {
            if (
                CheckIntersect(group.Range1, group.Range2) ||
                CheckIntersect(group.Range2, group.Range1)
            )
            {
                {
                    count++;
                }
            }
        }

        return count;
    }

    public int Part2(string input)
    {
        var groups = ParseInput(input);
        int count = 0;
        foreach (var group in groups)
        {
            if (
                CheckIntersect(group.Range1, group.Range2) ||
                CheckIntersect(group.Range2, group.Range1) ||
                CheckOverlap(group.Range1, group.Range2) ||
                CheckOverlap(group.Range2, group.Range1)
            )
            {
                {
                    count++;
                }
            }
        }

        return count;
    }
}

record ElfRange(int From, int To);

record ElfGroup(ElfRange Range1, ElfRange Range2);
