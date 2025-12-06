using Solutions.Base;
using Solutions.Extensions;

namespace Solutions.Days;


enum Direction { N, E, S, W }

class BoolArray(int maxX, int maxY)
{
    private readonly byte[] _array = new byte[maxX * maxY];

    public bool this[int x, int y]
    {
        get => _array[y + x * maxY] == 1;
        set => _array[y + x * maxY] = (byte)(value ? 1 : 0);
    }
}

class BoolDimArray(int maxX, int maxY)
{
    private readonly int _maxX = maxX;
    private readonly int _maxY = maxY;
    private readonly byte[] _array = new byte[maxX * maxY * 4];

    public bool this[int x, int y, Direction d]
    {
        get => _array[y * 4 + x * _maxY * 4 + (int)d] == 1;
        set => _array[y * 4 + x * _maxY * 4 + (int)d] = (byte)(value ? 1 : 0);
    }

    public BoolDimArray(BoolDimArray source) : this(source._maxX, source._maxY)
    {
        Array.Copy(source._array, _array, _array.Length);
    }
}

public class Day062 : IDay<int>
{
    public int Part1(string input)
    {
        var lines = input.SplitOnLines(removeEmpty: true).ToArray();
        var (obstructions, guard, dim) = Parse(lines);

        return GetPath(obstructions, guard, dim).Select(p => (p.x, p.y)).ToHashSet().Count;
    }

    public int Part2(string input)
    {
        var lines = input.SplitOnLines(removeEmpty: true).ToArray();
        var (obstructions, guard, dim) = Parse(lines);

        var originalPath = GetPath(obstructions, guard, dim);
        var path = new Stack<(int x, int y, Direction d)>(originalPath);
        var seen = new BoolDimArray(dim.maxX, dim.maxY);
        foreach (var p in originalPath) seen[p.x, p.y, p.d] = true;

        var loops = 0;

        // travese the path backwards 
        // so we have an easy bookkeeping of seen positions
        // without copying
        while (path.Count > 1)
        {
            // remove the position from the path
            var (x, y, d) = path.Pop();

            // remove the position from seen
            seen[x, y, d] = false;

            var obs = HashCode.Combine(x, y);

            if (obstructions[x, y]) continue;

            // don't add positions that are already in the path
            if (d != Direction.N && seen[x, y, Direction.N]) continue;
            if (d != Direction.E && seen[x, y, Direction.E]) continue;
            if (d != Direction.S && seen[x, y, Direction.S]) continue;
            if (d != Direction.W && seen[x, y, Direction.W]) continue;

            // add a obstruction at this position
            obstructions[x, y] = true;

            if (IsLoopPath(obstructions, path.Peek(), dim, new BoolDimArray(seen)))
            {
                loops++;
            }

            // remove the obstruction
            obstructions[x, y] = false;
        }

        return loops;
    }
    
    static List<(int x, int y, Direction d)> GetPath(BoolArray obstructions, (int x, int y, Direction d) guard, (int maxX, int maxY) dim)
    {
        var (x, y, d) = guard;
        List<(int x, int y, Direction d)> path = [];
        path.Add((x, y, d));

        while (true)
        {
            var (nextX, nextY) = NextPosition(x, y, d);

            if (OutOfBounds(nextX, nextY, dim)) break;

            if (obstructions[nextX, nextY])
            {
                d = Rotate90(d);
            }
            else
            {
                x = nextX;
                y = nextY;
            }

            path.Add((x, y, d));
        }

        return path;
    }

    static bool IsLoopPath(BoolArray obstructions, (int x, int y, Direction d) guard, (int maxX, int maxY) dim, BoolDimArray seen)
    {
        var (x, y, d) = guard;
        seen[x, y, d] = true;

        while (true)
        {
            var (nextX, nextY) = NextPosition(x, y, d);

            if (OutOfBounds(nextX, nextY, dim)) return false;

            if (obstructions[nextX, nextY])
            {
                d = Rotate90(d);
            }
            else
            {
                if (seen[nextX, nextY, d]) return true;
                x = nextX;
                y = nextY;
            }

            seen[x, y, d] = true;
        }
    }

    static Direction Rotate90(Direction d) => d = (Direction)(((int)d + 1) % 4);

    static bool OutOfBounds(int x, int y, (int maxX, int maxY) dim) => x < 0 || x >= dim.maxX || y < 0 || y >= dim.maxY;

    static (int x, int y) NextPosition(int x, int y, Direction d) => d switch
    {
        Direction.N => (x, y - 1),
        Direction.E => (x + 1, y),
        Direction.S => (x, y + 1),
        Direction.W => (x - 1, y),
        _ => throw new InvalidOperationException()
    };

    static (BoolArray obstructions, (int x, int y, Direction d) guard, (int maxX, int maxY) dim)
        Parse(string[] lines)
    {
        var obstructions = new BoolArray(lines[0].Length, lines.Length);
        (int x, int y, Direction d) guard = (0, 0, Direction.N);

        for (var y = 0; y < lines.Length; y++)
        for (var x = 0; x < lines[y].Length; x++)
        {
            switch (lines[y][x])
            {
                case '#': obstructions[x, y] = true; break;
                case '^': guard = (x, y, Direction.N); break;
            }
        }

        return (obstructions, guard, (lines[0].Length, lines.Length));
    }
}