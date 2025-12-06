using Solutions.Base;
using Solutions.Extensions;

namespace Solutions.Days;

public static class StringExtensions {
    public static bool IsLower(this string input)
    {
        return input.ToCharArray().All(char.IsLower);
    }
}

public class Day12: IDay<int>
{
    private Dictionary<string, List<string>> ParseInput(string input)
    {
        var nodes = new Dictionary<string, List<string>>();
        var lines = input
            .ToStringList().Select(l => l.Split('-').ToArray());

        foreach (var lineNodes in lines)
        {
            if (!nodes.ContainsKey(lineNodes[0]))
                nodes.Add(lineNodes[0], new());
            if (!nodes.ContainsKey(lineNodes[1]))
                nodes.Add(lineNodes[1], new ());

            nodes[lineNodes[0]].Add(lineNodes[1]);
            nodes[lineNodes[1]].Add(lineNodes[0]);
        }

        return nodes;
    }

    private int Paths(Dictionary<string, List<string>> nodes, string currentNode, List<string> visitedNodes, bool allowDoubles)
    {
        if (currentNode == "end")
            return 1;

        if (currentNode.IsLower())
            visitedNodes.Add(currentNode);

        var total = 0;
        foreach (var neighbor in nodes[currentNode])
        {
            if (!visitedNodes.Contains(neighbor) ||
                allowDoubles && visitedNodes.GroupBy(n => n).Max(n => n.Count()) == 1 && neighbor != "start")
            {
                total += Paths(nodes, neighbor, visitedNodes.ToList(), allowDoubles);
            }
        }

        return total;
    }

    public int Part1(string input)
    {
        var nodes = ParseInput(input);
        return Paths(nodes, "start", new(), false);
    }

    public int Part2(string input)
    {
        var nodes = ParseInput(input);
        return Paths(nodes, "start", new(), true);
    }
}
