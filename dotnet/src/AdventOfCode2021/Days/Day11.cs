using Solutions.Base;
using Solutions.Extensions;

namespace Solutions.Days;

public class Day11: IDay<int>
{
    private readonly List<(int X, int Y)> _offsets = new()
    {
        (0, -1),
        (1, -1),
        (1, 0),
        (1, 1),
        (0, 1),
        (-1, 1),
        (-1, 0),
        (-1, -1),
    };

    private Dictionary<(int X, int Y), int> ParseInput(string input)
    {
        return input
            .ToStringList()
            .SelectMany((l, y) => l.ToCharArray().Select((height, x) => (y, x, height)))
            .ToDictionary(c => (c.x, c.y), c => int.Parse(c.height.ToString()));
    }

    private (Dictionary<(int, int), int> endGrid, int flashes) Round(Dictionary<(int, int), int> startGrid)
    {
        var endGrid = startGrid.ToDictionary(i => i.Key, i => i.Value + 1);

        var flashedSquid = new HashSet<(int X, int Y)>();
        var flashingSquid = new Queue<(int X, int Y)>(endGrid.Where(i => i.Value > 9).Select(i => i.Key));

        while (flashingSquid.Count > 0)
        {
            var currentSquid = flashingSquid.Dequeue();
            flashedSquid.Add(currentSquid);

            var surroundingSquidLocations = _offsets
                .Select(l => (X: l.X + currentSquid.X, Y: l.Y + currentSquid.Y))
                .Where(l => endGrid.ContainsKey(l) && !flashedSquid.Contains(l) && !flashingSquid.Contains(l))
                .ToList();

            foreach (var ssl in surroundingSquidLocations.Where(ssl => ++endGrid[ssl] > 9))
            {
                flashingSquid.Enqueue(ssl);
            }
        }

        foreach (var squid in flashedSquid)
        {
            endGrid[squid] = 0;
        }

        return (endGrid, flashedSquid.Count);
    }

    public int Part1(string input)
    {
        var flashes = 0;
        var grid = ParseInput(input);
        for (var i = 0; i < 100; i++)
        {
            (grid, var roundFlashes) = Round(grid);
            flashes += roundFlashes;
        }

        return flashes;
    }

    public int Part2(string input)
    {
        var grid = ParseInput(input);
        var flashes = 0;
        var rounds = 0;

        while (flashes != 100)
        {
            (grid, flashes) = Round(grid);
            rounds++;
        }

        return rounds;
    }
}
