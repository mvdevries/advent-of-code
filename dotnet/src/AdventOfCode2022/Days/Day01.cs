using System.Collections;
using Solutions.Base;
using Solutions.Extensions;

namespace Solutions.Days;

public class Day01: IDay<int>
{
    private List<List<int>> ParseInput(string input)
    {
        return input.ToStringList("\n\n").Select(elfs => elfs.ToNumberList().ToList()).ToList();
    }

    public int Part1(string input)
    {
        var calories = ParseInput(input);

        var totalCalPerElf = calories.Select(calPerElf => calPerElf.Aggregate(0, (acc, cal) => acc + cal));
        return totalCalPerElf.Max();
    }

    public int Part2(string input)
    {
        var calories = ParseInput(input);

        var totalCalPerElf = calories.Select(calPerElf => calPerElf.Aggregate(0, (acc, cal) => acc + cal));
        return totalCalPerElf.OrderByDescending(p => p).Take(3).Aggregate(0, (acc, cal) => acc + cal);
    }
}
