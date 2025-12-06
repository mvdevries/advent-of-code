using System.Text.RegularExpressions;
using Solutions.Base;
using Solutions.Extensions;

namespace Solutions.Days;

public partial class Day03 : IDay<int>
{
    private static readonly Regex MuliplyPattern =
        MultiplyRegex();
    private static readonly Regex DoDontBlockPattern =
        DoDontRegex();
    
    private static IEnumerable<(int left, int right)> FindMultiplies(string line)
    {
        var matches = MuliplyPattern.Matches(line);
        return matches.Select(match => (int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value)));
    }
    
    public int Part1(string input)
    {
        var line = input.RemoveWhitespace();
        return FindMultiplies(line).Select(multiply => multiply.right * multiply.left).Sum();
    }
    
    private static IEnumerable<string> FindDoDontBlocks(string line)
    {
        return DoDontBlockPattern.Matches(line).Select(match => match.Value);
    }

    public int Part2(string input)
    {
        var line = input.RemoveWhitespace();
        return FindDoDontBlocks(line)
            .SelectMany(FindMultiplies)
            .Select(multiply => multiply.right * multiply.left)
            .Sum();
    }
        
    [GeneratedRegex(@"mul\(([0-9]{1,3}),([0-9]{1,3})\)", RegexOptions.Compiled)]
    private static partial Regex MultiplyRegex();
    
    [GeneratedRegex(@"(?<do>do\(\))(?<between>.*?)(?<dont>don't\(\))|^(?<start_to_dont>.*?)don't\(\)", RegexOptions.Compiled)]
    private static partial Regex DoDontRegex();
}