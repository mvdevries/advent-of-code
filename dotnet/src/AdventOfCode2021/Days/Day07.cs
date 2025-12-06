using Solutions.Base;
using Solutions.Extensions;

namespace Solutions.Days;

public class Day07: IDay<int>
{
    public int Part1(string input)
    {
        var crapPositions = input.ToNumberList(",").ToArray();
        var median = Median(crapPositions);

        return crapPositions.Select(cp => Math.Abs(cp - median)).Sum();
    }

    private int Median(int[] numbers)
    {
        var middle = (int)Math.Floor((double)numbers.Length / 2);
        var sortedNumbers = numbers.ToArray();
        Array.Sort(sortedNumbers);

        if (sortedNumbers.Length % 2 != 0)
        {
            return sortedNumbers[middle];
        }

        return (sortedNumbers[middle - 1] + sortedNumbers[middle]) / 2;
    }

    // 100347068 Round <--
    // 100347031 Floor? <--
    public int Part2(string input)
    {
        var crapPositions = input.ToNumberList(",").ToArray();
        var mean = Mean(crapPositions);

        return crapPositions.Select(cp => TriangularNumber(Math.Abs(cp - mean))).Sum();
    }

    private int Mean(int[] numbers)
    {
        // Floor ipv Round??
        return (int)Math.Floor(numbers.Average());
    }

    private int TriangularNumber(int n)
    {
        return n * (n + 1) / 2;
    }
}
