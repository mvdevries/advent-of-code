using System.Collections;
using Solutions.Base;
using Solutions.Extensions;

namespace Solutions.Days;

file enum Direction
{
    None,
    Up,
    Down
}

public class Day02 : IDay<int>
{
    private bool IsDiffSafe(int a, int b)
    {
        return Math.Abs(a - b) is >= 1 and <= 3;
    }
    
    private bool Part1CheckLine(IEnumerable<int> report)
    {
        var items = report.ToList();
        var direction = Direction.None;
        for (var i = 0; i < items.Count; i++)
        {
            var currentItem = items[i];
            
            if (i == items.Count - 1)
            {
                continue;
            }

            var nextItem = items[i + 1];
            
            if (!IsDiffSafe(currentItem, nextItem))
            {
                return false;
            }
            
            if (nextItem > currentItem && direction == Direction.None) // New Up
            {
                direction = Direction.Up;
                continue;
            }
            
            if (nextItem < currentItem && direction == Direction.None) // New Down
            {
                direction = Direction.Down;
                continue;
            }
            
            if (direction == Direction.Up && nextItem < currentItem) // Down
            {
                return false;
            }
            
            if (direction == Direction.Down && nextItem > currentItem) // Up
            {
                return false;
            }
        }
        
        return true;
    }
    
    public int Part1(string input)
    {
        return input
            .SplitOnLines(removeEmpty: true)
            .Select(report => report.SplitOnSpace(removeEmpty: true).Select(int.Parse))
            .Count(Part1CheckLine);
    }
    
    private static IEnumerable<List<int>> Permute(IEnumerable<int> report)
    {
        var reportItems = report.ToList();
        for (var i = 0; i < reportItems.Count; i++)
        {
            var variant = new List<int>(reportItems);
            variant.RemoveAt(i);
            yield return variant;
        }
    }

    public int Part2(string input)
    {
        return input
            .SplitOnLines(removeEmpty: true)
            .Select(report => report.SplitOnSpace(removeEmpty: true).Select(int.Parse))
            .Select(Permute)
            .Count(reportSet => reportSet.Any(Part1CheckLine));
    }
}