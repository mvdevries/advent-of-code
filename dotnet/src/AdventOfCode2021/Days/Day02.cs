using Solutions.Base;
using Solutions.Extensions;

namespace Solutions.Days;

public class Day02: IDay<int>
{
    private record Command (Direction Direction, int Value);

    private enum Direction
    {
        Forward,
        Down,
        Up
    }

    public int Part1(string input)
    {
        var (horizontal, depth) = input
            .ToPartsList()
            .Select(p => new Command(p[0].ToEnum<Direction>(true), int.Parse(p[1])))
            .Aggregate((horizontal: 0, depth: 0), (acc, c) =>
            {
                if (c.Direction == Direction.Forward)
                {
                    acc.horizontal += c.Value;
                }

                if (c.Direction == Direction.Down)
                {
                    acc.depth += c.Value;
                }

                if (c.Direction == Direction.Up)
                {
                    acc.depth -= c.Value;
                }

                return acc;
            });
        return horizontal * depth;
    }

    public int Part2(string input)
    {
        var (horizontal, depth, _) = input
            .ToPartsList()
            .Select(p => new Command(p[0].ToEnum<Direction>(true), int.Parse(p[1])))
            .Aggregate((horizontal: 0, depth: 0, aim: 0), (acc, c) =>
            {
                if (c.Direction == Direction.Forward)
                {
                    acc.horizontal += c.Value;
                    acc.depth += (acc.aim * c.Value);
                }

                if (c.Direction == Direction.Down)
                {
                    acc.aim += c.Value;
                }

                if (c.Direction == Direction.Up)
                {
                    acc.aim -= c.Value;
                }

                return acc;
            });
        return horizontal * depth;
    }
}
