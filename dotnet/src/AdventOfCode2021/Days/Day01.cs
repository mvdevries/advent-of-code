using Solutions.Base;
using Solutions.Extensions;

namespace Solutions.Days;

public class Day01: IDay<int>
{
    public int Part1(string input)
    {
        return input
            .ToNumberList()
            .PairUp(2)
            .Aggregate(0, (acc, pair) => pair[1] > pair[0] ? acc + 1 : acc);
    }

    public int Part2(string input)
    {
        return input.ToNumberList()
            .PairUp(3)
            .Select(pair => pair.Sum())
            .PairUp(2)
            .Aggregate(0, (acc, pair) => pair[1] > pair[0] ? acc + 1 : acc);
    }
}
