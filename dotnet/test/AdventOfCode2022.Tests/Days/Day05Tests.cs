using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Solutions.Days;
using Xunit;

namespace Solutions.Tests.Days;

public class UnitTest05: IAsyncLifetime
{
    private readonly Day05 _day = new();
    private string? _input;

    private const string HandInput =
@"[ ] [D] [ ]
[N] [C] [ ]
[Z] [M] [P]
 1   2   3

move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2";

    public async Task InitializeAsync()
    {
        _input = await File.ReadAllTextAsync("./Inputs/Day05.txt");
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    [Fact]
    public void HandTestPart1()
    {
        var answer = _day.Part1(HandInput);
        Assert.Equal("CMZ", answer);
    }

    [Fact]
    public void Part1()
    {
        var answer = _day.Part1(_input!);
        Assert.Equal("WCZTHTMPS", answer);
    }

    [Fact]
    public void HandTestPart2()
    {
        var answer = _day.Part2(HandInput);
        Assert.Equal("MCD", answer);
    }

    [Fact]
    public void Part2()
    {
        var answer = _day.Part2(_input!);
        Assert.Equal("BLSGJSDTS", answer);
    }
}
