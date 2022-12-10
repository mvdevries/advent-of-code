using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Solutions.Days;
using Xunit;

namespace Solutions.Tests.Days;

public class UnitTest03: IAsyncLifetime
{
    private readonly Day03 _day = new();
    private string? _input;

    private const string HandInput =
@"vJrwpWtwJgWrhcsFMMfFFhFp
jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
PmmdzqPrVvPwwTWBwg
wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
ttgJtRGJQctTZtZT
CrZsJsPPZsGzwwsLwLmpwMDw
";

    public async Task InitializeAsync()
    {
        _input = await File.ReadAllTextAsync("./Inputs/Day03.txt");
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    [Fact]
    public void HandTestPart1()
    {
        var answer = _day.Part1(HandInput);
        Assert.Equal(157, answer);
    }

    [Fact]
    public void Part1()
    {
        var answer = _day.Part1(_input!);
        Assert.Equal(8139, answer);
    }

    [Fact]
    public void HandTestPart2()
    {
        var answer = _day.Part2(HandInput);
        Assert.Equal(70, answer);
    }

    [Fact]
    public void Part2()
    {
        var answer = _day.Part2(_input!);
        Assert.Equal(2668, answer);
    }
}
