using System.Text.RegularExpressions;
using Solutions.Base;
using Solutions.Extensions;

namespace Solutions.Days;

public partial class Day04 : IDay<int>
{
    private static readonly (int x, int y)[] Directions8Ways =
    [
        (-1, -1), (0, -1), (1, -1),
        (-1,  0),          (1,  0),
        (-1,  1), (0,  1), (1,  1)
    ];
    
    private static readonly (int x, int y)[] Directions4WaysX =
    [
        (-1, -1), (1, -1),
        (-1,  1), (1,  1)
    ];
    
    private static bool GridIsEqual(char[][] grid, int x, int y, char character)
    {
        if (x < 0 || x >= grid.Length || y < 0 || y >= grid[x].Length)
        {
            return false;
        }
            
        return grid[x][y] == character;
    }
    
    private static int Find8WaysXMAS(char[][] grid, int x, int y)
    {
        var count = 0;
        foreach (var (dx, dy) in Directions8Ways)
        {
            var nx = x + dx;
            var ny = y + dy;
            var isMas = GridIsEqual(grid, nx, ny, 'M') &&
                        GridIsEqual(grid, nx + dx, ny + dy, 'A') &&
                        GridIsEqual(grid, nx + dx + dx, ny + dy + dy, 'S');
            if (isMas)
            {
                count++;
            }
        }

        return count;
    }


    private static char[][] ParseInput(string input)
    {
        var lines = input.SplitOnLines(removeEmpty: true);
        return lines.Select(l => l.ToArray()).ToArray();
    }
    
    private static bool HasFind4WayMAS(char[][] grid, int x, int y)
    {
        var count = 0;
        foreach (var (dx, dy) in Directions4WaysX)
        {
            var isMas = GridIsEqual(grid, x + dx, y + dy, 'M') &&
                        GridIsEqual(grid, x - dx, y - dy, 'S');
            if (isMas)
            {
                count++;
            }
        }

        return count == 2;
    }
    
    public int Part1(string input)
    {
        var grid = ParseInput(input);
        
        var count = 0;
        
        for(var x = 0; x < grid.Length; x++)
        {
            for(var y = 0; y < grid[x].Length; y++)
            {
                var character = grid[x][y];
                
                if (character == 'X')
                {
                    count += Find8WaysXMAS(grid, x, y);
                }
            }
        }
        
        return count;
    }

    public int Part2(string input)
    {
        var lines = input.SplitOnLines(removeEmpty: true);
        var grid = lines.Select(l => l.ToArray()).ToArray();
        
        var count = 0;
        
        for(var x = 0; x < grid.Length; x++)
        {
            for(var y = 0; y < grid[x].Length; y++)
            {
                var character = grid[x][y];
                
                if (character == 'A' && HasFind4WayMAS(grid, x, y))
                {
                    count++;
                }
            }
        }
        
        return count;
    }
}