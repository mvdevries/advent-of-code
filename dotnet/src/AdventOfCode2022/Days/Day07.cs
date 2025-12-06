using CommunityToolkit.HighPerformance;
using Solutions.Base;
using Solutions.Extensions;

namespace Solutions.Days;

public class Day07 : IDay<long>
{
    private Directory ParseInput(string input)
    {
        Directory root = new Directory("/", null, new Dictionary<string, Node?>());
        Directory? current = null;
        foreach (var command in input.ToStringList())
        {
            if (command.StartsWith(@"$ cd .."))
            {
                current = current!.Parent;
            }
            else if (command.StartsWith(@"$ cd "))
            {
                var name = command.Split(" ").Last().Trim();
                current = (Directory)current!.Children[name]!;
            }
            else if (command.StartsWith("dir"))
            {
                var name = command.Split(" ").Last().Trim();
                current!.Children[name] = new Directory(name, current, new());
            }
            else if (Char.IsNumber(command[0]))
            {
                var fileSplit = command.Split(" ");
                var size = long.Parse(fileSplit[0]);
                var name = fileSplit[1];
                current!.Children[name] = new File(name, size);
            }
        }

        return root;
    }

    private void CalcDirectorySize(Directory? directory, Dictionary<Directory, long> sizes)
    {
        long size = 0;
        foreach (var child in directory!.Children)
        {
            switch (child.Value)
            {
                case File f:
                    size += f.Size;
                    break;
                case Directory d:
                    CalcDirectorySize(d, sizes);
                    size += sizes[d];
                    break;
            }
        }
        sizes[directory] = size;
    }

    public long Part1(string input)
    {
        var root = ParseInput(input);
        var directorySizes = new Dictionary<Directory, long>();
        CalcDirectorySize(root, directorySizes);

        return directorySizes.Values.Where(size => size <= 100_000).Sum();
    }

    public long Part2(string input)
    {
        var root = ParseInput(input);
        var directorySizes = new Dictionary<Directory, long>();
        CalcDirectorySize(root, directorySizes);

        return directorySizes.Values
            .Where(size => size >= directorySizes[root] - 40_000_000)
            .Min();
    }
}

record Node(string Name);

record Directory(string Name, Directory? Parent, Dictionary<string, Node?> Children) : Node(Name);

record File(string Name, long Size) : Node(Name);
