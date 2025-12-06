using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Solutions.Days;
using Xunit;

namespace Solutions.XTests.Days;

public class UnitTest03: IAsyncLifetime
{
    private readonly Day03 _day03 = new();
    private string? _input;
    private const string _handInput = @"
00100
11110
10110
10111
10101
01111
00111
11100
10000
11001
00010
01010";

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
        var answer = _day03.Part1(_handInput);
        Debug.WriteLine(answer);
    }

    [Fact]
    public void Part1()
    {
        var answer = _day03.Part1(_input!);
        Debug.WriteLine(answer);
        Assert.Equal(3009600, answer);
    }

    [Fact]
    public void HandTestPart2()
    {
        var answer = _day03.Part2(_handInput);
        Debug.WriteLine(answer);
    }

    [Fact]
    public void Part2()
    {
        var answer = _day03.Part2(_input!);
        Debug.WriteLine(answer);
        Assert.Equal(6940518, answer);
    }
}
