using Solutions.Base;
using Solutions.Extensions;

namespace Solutions.Days;

public class Day07 : IDay<long>
{ 
    private List<(long sum, List<long> values)> ParseInput(string input)
    {
        var lines = input.SplitOnLines(removeEmpty: true);
        return lines
            .Select(l => l.SplitTrimIn2Parts(":"))
            .Select(l => (
                long.Parse(l.part1),
                l.part2.SplitOnSpace(removeEmpty: true).Select(long.Parse).ToList())).ToList();
    }

    private bool TestOperations(List<long> values, long sum, List<Func<long, long, long>> operations)
    {
        if (values.Count == 1)
        {
            return values[0] == sum;
        }

        return operations.Any(operation =>
        {
            var nextValues = values[1..];
            nextValues[0] = operation(values[0], values[1]);
            return TestOperations(nextValues, sum, operations);
        });
    }
    
    public long Part1(string input)
    {
        var equations = ParseInput(input);
        var count = 0L;
        foreach (var equation in equations)
        {
            var sum = equation.sum;
            var values = equation.values;
            var operations = new List<Func<long, long, long>>
            {
                (a, b) => a + b,
                (a, b) => a * b,
            };
            if (TestOperations(values, sum, operations))
            {
                count += sum;
            }
        }

        return count;
    }

    public long Part2(string input)
    {
        var equations = ParseInput(input);
        var count = 0L;
        foreach (var equation in equations)
        {
            var sum = equation.sum;
            var values = equation.values;
            var operations = new List<Func<long, long, long>>
            {
                (a, b) => a + b,
                (a, b) => a * b,
                (a, b) => long.Parse($"{a}{b}")
            };
            if (TestOperations(values, sum, operations))
            {
                count += sum;
            }
        }

        return count;
    }
}