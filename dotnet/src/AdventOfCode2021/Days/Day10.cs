using Solutions.Base;
using Solutions.Extensions;

namespace Solutions.Days;

public class Day10: IDay<long>
{
    private readonly Dictionary<char, char> _pairsMap = new()
    {
        {'(', ')'},
        {'<', '>'},
        {'[', ']'},
        {'{', '}'},
    };

    private readonly Dictionary<char, int> _scoreMap1 = new()
    {
        { 'n', 0 },
        { ')', 3 },
        { ']', 57 },
        { '}', 1197 },
        { '>', 25137 },
    };

    private readonly Dictionary<char, int> _scoreMap2 = new()
    {
        { ')', 1 },
        { ']', 2 },
        { '}', 3 },
        { '>', 4 },
    };

    private char FindSyntaxErrorInLine(string line)
    {
        var stack = new Stack<char>();

        foreach (var ch in line)
        {
            if (!_pairsMap.ContainsKey(ch))
            {
                if (_pairsMap[stack.Peek()] != ch)
                {
                    return ch;
                }

                stack.Pop();
                continue;
            }

            stack.Push(ch);
        }

        return 'n';
    }

    private long CloseValidLines(string line)
    {
        var stack = new Stack<char>();

        foreach (var ch in line)
        {
            if (!_pairsMap.ContainsKey(ch))
            {
                stack.Pop();
                continue;
            }

            stack.Push(ch);
        }

        return stack.Aggregate(0L, (acc, ch) => acc * 5 + _scoreMap2[_pairsMap[ch]]);
    }


    public long Part1(string input)
    {
        return input
            .ToStringList()
            .Select(FindSyntaxErrorInLine)
            .Select(wrongChar => _scoreMap1[wrongChar])
            .Sum();
    }

    private long Median(long[] numbers)
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

    public long Part2(string input)
    {
        return Median( input
            .ToStringList()
            .Where(l => FindSyntaxErrorInLine(l) == 'n')
            .Select(CloseValidLines)
            .OrderBy(s => s)
            .ToArray());
    }
}
