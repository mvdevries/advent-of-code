using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Solutions.Days;
using Xunit;

namespace Solutions.Tests.Days;

public class UnitTest07: IAsyncLifetime
{
    private readonly Day07 _day = new();
    private string? _input;

    private const string HandInput =
@"$ cd /
$ ls
dir a
14848514 b.txt
8504156 c.dat
dir d
$ cd a
$ ls
dir e
29116 f
2557 g
62596 h.lst
$ cd e
$ ls
584 i
$ cd ..
$ cd ..
$ cd d
$ ls
4060174 j
8033020 d.log
5626152 d.ext
7214296 k";

    public async Task InitializeAsync()
    {
        _input = await File.ReadAllTextAsync("./Inputs/Day07.txt");
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    [Fact]
    public void HandTestPart1()
    {
        var answer = _day.Part1(HandInput);
        Assert.Equal(95437, answer);
    }

    [Fact]
    public void Part1()
    {
        var answer = _day.Part1(_input!);
        Assert.Equal(1642503, answer);
    }

    [Fact]
    public void HandTestPart2()
    {
        var answer = _day.Part2(HandInput);
        Assert.Equal(24933642, answer);
    }

    [Fact]
    public void Part2()
    {
        var answer = _day.Part2(_input!);
        Assert.Equal(6999588, answer);
    }
}
