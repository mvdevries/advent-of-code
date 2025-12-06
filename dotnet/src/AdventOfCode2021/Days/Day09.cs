using Solutions.Base;
using Solutions.Extensions;

namespace Solutions.Days;

public class Day09: IDay<int>
{
    private readonly List<(int X, int Y)> _offsets = new()
    {
        (-1, 0),
        (0, 1),
        (0, -1),
        (1, 0),
    };

    private Dictionary<(int X, int Y), int> ParseInput(string input)
    {
        return input
            .ToStringList()
            .SelectMany((l, y) => l.ToCharArray().Select((height, x) => (y, x, height)))
            .ToDictionary(c => (c.x, c.y), c => int.Parse(c.height.ToString()));
    }

    private List<(int X, int Y)> GetLowPoints(Dictionary<(int X, int Y), int> heights)
    {
        var lowPoints = new List<(int, int)>();
        foreach (var coord in heights.Keys)
        {
            var neighbours = _offsets
                .Select(o => (X: o.X + coord.X, Y: o.Y + coord.Y))
                .Where(heights.ContainsKey)
                .ToList();

            if (neighbours.All(x => heights[coord] < heights[x]))
            {
                lowPoints.Add(coord);
            }
        }

        return lowPoints;
    }

    public int Part1(string input)
    {
        var heights = ParseInput(input);
        return GetLowPoints(heights).Select(p => heights[p] + 1).Sum();
    }

    private int GetBasinSize(Dictionary<(int X, int Y), int> heights, (int X, int Y) location)
    {
        var heightsToCheck = new Queue<(int X, int Y)>();
        heightsToCheck.Enqueue(location);

        var checkedHeights = new List<(int X, int Y)>();

        while (heightsToCheck.Count > 0)
        {
            var current = heightsToCheck.Dequeue();
            checkedHeights.Add(current);

            var offsetLocations = _offsets
                .Select(x => (X: x.X + current.X, Y: x.Y + current.Y))
                .Where(x => heights.ContainsKey(x) && heights[x] != 9 && !checkedHeights.Contains(x) && !heightsToCheck.Contains(x))
                .ToList();

            foreach (var offsetLocation in offsetLocations)
            {
                heightsToCheck.Enqueue(offsetLocation);
            }
        }

        return checkedHeights.Count;
    }

    public int Part2(string input)
    {
        var heights = ParseInput(input);
        var lowPoints = GetLowPoints(heights);

        return lowPoints
            .Select(point => GetBasinSize(heights, point))
            .OrderByDescending(p => p)
            .Take(3)
            .Aggregate((x,y) => x * y);
    }
}
