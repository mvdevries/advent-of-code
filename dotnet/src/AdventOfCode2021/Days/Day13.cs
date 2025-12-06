using System.Text;
using Solutions.Base;
using Solutions.Extensions;

namespace Solutions.Days;

public class Day13: IDay<string>
{
    private enum Direction { X, Y }

    private (HashSet<(int X, int Y)>, List<(Direction, int)>) ParseInput(string input)
    {
        var dots = input
            .ToStringList()
            .TakeWhile(line => !string.IsNullOrWhiteSpace(line))
            .Select(line => line.Split(','))
            .Select(parts => (int.Parse(parts[0]), int.Parse(parts[1])))
            .Aggregate(new HashSet<(int, int)>(), (acc, dot) =>
            {
                acc.Add(dot);
                return acc;
            });

        var folds = input
            .ToStringList()
            .Skip(dots.Count + 1)
            .Select(line => line.Split(' ').Last())
            .Select(line => (line[0] == 'x' ? Direction.X : Direction.Y, int.Parse(line.Substring(2))))
            .ToList();

        return (dots, folds);
    }

    HashSet<(int, int)> Fold(HashSet<(int, int)> dots, (Direction, int) fold)
    {
        var (direction, position) = fold;
        var newDots = new HashSet<(int, int)>();
        foreach (var (x, y) in dots)
        {
            var newX = direction == Direction.X && x > position ? (position - (x - position)) : x;
            var newY = direction == Direction.Y && y > position ? (position - (y - position)) : y;
            newDots.Add((newX, newY));
        }
        return newDots;
    }

    public string Part1(string input)
    {
        var (dots, folds) = ParseInput(input);
        return Fold(dots, folds.First()).Count.ToString();
    }

    public string Part2(string input)
    {
        var (dots, folds) = ParseInput(input);
        var sb = new StringBuilder();

        var finalCode = folds.Aggregate(dots, Fold);
        var maxX = finalCode.Select(pt => pt.Item1).Max();
        var maxY = finalCode.Select(pt => pt.Item2).Max();

        for (var y = 0; y <= maxY; y++)
        {
            for (var x = 0; x <= maxX; x++)
            {
                sb.Append(finalCode.Contains((x, y)) ? '#' : ' ');
            }
            sb.Append('\n');
        }

        return sb.ToString();
    }
}
