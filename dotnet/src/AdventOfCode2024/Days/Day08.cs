using Solutions.Base;
using Solutions.Extensions;

namespace Solutions.Days;

public class Day08 : IDay<int>
{ 
    private static List<((int x, int y) first, (int x, int y) second)> MakePairs(List<(int x, int y)> items)
    {
        var pairs = new List<((int, int), (int, int))>();

        for (var i = 0; i < items.Count; i++)
        {
            for (var j = i + 1; j < items.Count; j++)
            {
                pairs.Add(((items[i].x, items[i].y), (items[j].x, items[j].y)));
            }
        }

        return pairs;
    }    
    private static (Dictionary<char, HashSet<(int, int)>>, int maxX, int maxY) ParseInput(string input)
    {
        var lines = input.SplitOnLines(removeEmpty: true).ToArray();
        var nodesOfType = new Dictionary<char, HashSet<(int, int)>>();
        var maxY = lines.Length - 1;
        var maxX = lines[0].Length - 1;

        for (var y = 0; y < lines.Length; y++)
        {
            for (var x = 0; x < lines[y].Length; x++)
            {
                if (lines[y][x] is '.')
                {
                    continue;
                }
                
                var type = lines[y][x];
                if (!nodesOfType.ContainsKey(type))
                {
                    nodesOfType.Add(type, [(x, y)]);
                    continue;
                }
                
                nodesOfType[type].Add((x, y));
            }
        }

        return (nodesOfType, maxX, maxY);
    }
    
    private static (int diffX, int diffY) GetDiffs((int x, int y) first, (int x, int y) second)
    {
        return (second.x - first.x, second.y - first.y);
    }
    
    private static int ComparePair((int x, int y) first, (int x, int y) second)
    {
        if (first.y != second.y)
        {
            return first.y.CompareTo(second.y);
        }

        // Als de y-coördinaten gelijk zijn, vergelijk op basis van de x-coördinaten
        return first.x.CompareTo(second.x);
    }
    
    private static ((int x, int y) First, (int x, int y) Second) OrderPair((int x, int y) node1, (int x, int y) node2)
    {
        return ComparePair(node1, node2) < 0 ? (node1, node2) : (node2, node1);
    }
    
    private static bool IsInRange(int x, int y, int maxX, int maxY)
    {
        return x >= 0 && x <= maxX && y >= 0 && y <= maxY;
    }

    private static HashSet<(int x, int y)> GetFirstAntiNodesForPair(
        (int x, int y) node1,
        (int x, int y) node2,
        int maxX, 
        int maxY)
    {
        var (first, second) = OrderPair(node1, node2);
        var (diffX, diffY) = GetDiffs(node1, node2);
        var antiNodes = new HashSet<(int, int)>();
        
        var beforeX = first.x - diffX;
        var beforeY = first.y - diffY;
        if (IsInRange(beforeX, beforeY, maxX, maxY))
        {
            antiNodes.Add((beforeX, beforeY));
        }
        
        var afterX = second.x + diffX;
        var afterY = second.y + diffY;
        if (IsInRange(afterX, afterY, maxX, maxY))
        {
            antiNodes.Add((afterX, afterY));
        }
       
        return antiNodes;
    }
    
    private static HashSet<(int, int)> GetAntiNodesForPair(
        (int x, int y) node1,
        (int x, int y) node2, 
        int maxX,
        int maxY)
    {
        var (first, second) = OrderPair(node1, node2);
        var (diffX, diffY) = GetDiffs(node1, node2);
        var antiNodes = new HashSet<(int, int)>();
        
        // Check for anti-nodes in the direction of the pair
        for (var i = 1; i < Math.Max(maxX, maxY); i++)
        {
            var beforeX = first.x - diffX * i;
            var beforeY = first.y - diffY * i;
            if (!IsInRange(beforeX, beforeY, maxX, maxY))
            {
                break;
            }
            
            antiNodes.Add((beforeX, beforeY));
        }
        
        // Check for anti-nodes in the opposite direction of the pair
        for (var i = 1; i < Math.Max(maxX, maxY); i++)
        {
            var afterX = second.x + diffX * i;
            var afterY = second.y + diffY * i;
            if (!IsInRange(afterX, afterY, maxX, maxY))
            {
                break;
            }
            
            antiNodes.Add((afterX, afterY));
        }
       
        return antiNodes;
    }
    
    public int Part1(string input)
    {
        var (nodes, maxX, maxY) = ParseInput(input);
        var antiNodes = new Dictionary<((int x1, int y1) first, (int x2, int y2) second, char Type), HashSet<(int x, int y)>>();
        
        foreach (var node in nodes)
        {
            var pairs = MakePairs(node.Value.ToList());
            foreach (var pair in pairs)
            {
                var antiNodesForPair = GetFirstAntiNodesForPair(
                    pair.first, 
                    pair.second, 
                    maxX, 
                    maxY);
                antiNodes.Add((pair.first, pair.second, node.Key), antiNodesForPair);
            }
        }
        
        var antiNodesTotal = new HashSet<(int x, int y)>();
        foreach(var antiNodesForPair in antiNodes)
        {
            antiNodesTotal.UnionWith(antiNodesForPair.Value);
        }

        return antiNodesTotal.Count;
    }

    public int Part2(string input)
    {
        var (nodes, maxX, maxY) = ParseInput(input);
        var antiNodes = new Dictionary<((int x1, int y1) first, (int x2, int y2) second, char Type), HashSet<(int x, int y)>>();
        
        foreach (var node in nodes)
        {
            var pairs = MakePairs(node.Value.ToList());
            foreach (var pair in pairs)
            {
                var antiNodesForPair = GetAntiNodesForPair(
                    pair.first, 
                    pair.second, 
                    maxX, 
                    maxY);
                antiNodes.Add((pair.first, pair.second, node.Key), antiNodesForPair);
            }
        }
        
        var antiNodesTotal = new HashSet<(int x, int y)>();
        foreach(var antiNodesForPair in antiNodes)
        {
            antiNodesTotal.UnionWith(antiNodesForPair.Value);
        }
        antiNodesTotal.UnionWith(nodes.Values.SelectMany(x => x));
        return antiNodesTotal.Count;
    }
}