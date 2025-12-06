using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Solutions.Days;
using Xunit;

namespace Solutions.Tests.Days;

public class UnitTest10: IAsyncLifetime
{
    private readonly Day10 _day = new();
    private string? _input;

    private const string HandInput = "";

    public async Task InitializeAsync()
    {
        _input = await File.ReadAllTextAsync("./Inputs/Day10.txt");
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    [Fact]
    public void HandTestPart1()
    {
        var answer = _day.Part1(HandInput);
        Assert.Equal(0, answer);
    }

    [Fact]
    public void Part1()
    {
        var answer = _day.Part1(_input!);
        Assert.Equal(0, answer);
    }

    [Fact]
    public void HandTestPart2()
    {
        var answer = _day.Part2(HandInput);
        Assert.Equal(0, answer);
    }

    [Fact]
    public void Part2()
    {
        var answer = _day.Part2(_input!);
        Assert.Equal(0, answer);
    }
}
