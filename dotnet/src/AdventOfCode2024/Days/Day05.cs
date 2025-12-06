using Solutions.Base;
using Solutions.Extensions;

namespace Solutions.Days;

internal class PageComparer((int, int)[] rules) : IComparer<int>
{
    public int Compare(int x, int y)
    {
        return rules.Contains((x, y)) ? -1 : rules.Contains((y, x)) ? 1 : 0;
    }
}

public class Day05 : IDay<int>
{ 
    private (PageComparer comparer, List<int[]> pages) ParseInput(string input)
    {
        var (ruleLines, orderingLines) = input.SplitTrimIn2Parts(separator: "\n\n");
        var rules = ruleLines
            .SplitOnLines()
            .Select(l => l.SplitTrimIn2Parts("|"))
            .Select(l => (first: int.Parse(l.part1), second: int.Parse(l.part2)))
            .ToArray();
        
        var orderings = orderingLines
            .SplitOnLines()
            .Select(l => l.Split(','))
            .Select(p => p.Select(int.Parse).ToArray())
            .ToList();

        return (new PageComparer(rules), orderings);
    }

    private static bool SortedCorrectly(int[] pages, PageComparer comparer)
    {
        return pages.SequenceEqual(pages.Order(comparer));
    }
    
    public int Part1(string input)
    {
        var (comparer, pagesList) = ParseInput(input);
        return pagesList
            .Where(pages => SortedCorrectly(pages, comparer))
            .Sum(x => x.ElementAt(x.Length / 2));
    }

    public int Part2(string input)
    {
        var (comparer, pagesList) = ParseInput(input);
        return pagesList
            .Where(pages => !SortedCorrectly(pages, comparer))
            .Sum(x =>  x.Order(comparer).ElementAt(x.Length / 2));
    }
}