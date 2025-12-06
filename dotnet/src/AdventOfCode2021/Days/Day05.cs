using System.Drawing;
using System.Text.RegularExpressions;
using Solutions.Base;
using Solutions.Extensions;

namespace Solutions.Days;

public class Day05: IDay<int>
{
    private record Line(Point P1, Point P2)
    {
        public bool IsHorizontalOrVertical
        {
            get
            {
                var dy = P2.Y - P1.Y;
                var dx = P2.X - P1.X;
                return dx == 0 || dy == 0;
            }
        }
    }

    private IEnumerable<Line> ParseInput(string input)
    {
        var regex = new Regex(@"(\d+),(\d+) -> (\d+),(\d+)");
        return input.ToStringList().Select(l =>
        {
            var match = regex.Match(l);
            return new Line(
                new Point(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value)),
                new Point(int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value))
            );
        });
    }

    public int Part1(string input)
    {
        var grid = new Dictionary<Point, int>();
        var lines = ParseInput(input).Where(l => l.IsHorizontalOrVertical);

        foreach(var (point, point2) in lines)
        {
            var xStep = point.X == point2.X ? 0 : point.X > point2.X ? -1 : 1;
            var yStep = point.Y == point2.Y ? 0 : point.Y > point2.Y ? -1 : 1;

            var currentX = point.X;
            var currentY = point.Y;

            do
            {
                var currentPoint = new Point(currentX, currentY);
                if (!grid.ContainsKey(currentPoint))
                    grid[currentPoint] = 0;

                grid[currentPoint]++;

                if ((currentX, currentY) == (point2.X, point2.Y))
                {
                    break;
                }

                currentX += xStep;
                currentY += yStep;
            }
            while (true);
        }

        return grid.Count(p => p.Value > 1);
    }

    public int Part2(string input)
    {
        var grid = new Dictionary<Point, int>();
        var lines = ParseInput(input);

        foreach(var (point, point2) in lines)
        {
            var xStep = point.X == point2.X ? 0 : point.X > point2.X ? -1 : 1;
            var yStep = point.Y == point2.Y ? 0 : point.Y > point2.Y ? -1 : 1;

            var currentX = point.X;
            var currentY = point.Y;

            do
            {
                var currentPoint = new Point(currentX, currentY);
                if (!grid.ContainsKey(currentPoint))
                    grid[currentPoint] = 0;

                grid[currentPoint]++;

                if ((currentX, currentY) == (point2.X, point2.Y))
                {
                    break;
                }

                currentX += xStep;
                currentY += yStep;
            }
            while (true);
        }

        return grid.Count(p => p.Value > 1);
    }
}
