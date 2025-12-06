using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Solutions.Days;
using Xunit;

namespace Solutions.XTests.Days;

public class UnitTest02: IAsyncLifetime
{
    private readonly Day02 _day02 = new();
    private string? _input;

    public async Task InitializeAsync()
    {
        _input = await File.ReadAllTextAsync("./Inputs/Day02.txt");
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    [Fact]
    public void HandTestPart1()
    {
        const string input = @"
forward 5
down 5
forward 8
up 3
down 8
forward 2";
        var answer = _day02.Part1(input);
        Debug.WriteLine(answer);
    }

    [Fact]
    public void Part1()
    {
        var answer = _day02.Part1(_input!);
        Debug.WriteLine(answer);
        Assert.Equal(2102357, answer);
    }

    [Fact]
    public void HandTestPart2()
    {
        const string input = @"
forward 5
down 5
forward 8
up 3
down 8
forward 2";
        var answer = _day02.Part2(input);
        Debug.WriteLine(answer);
    }

    [Fact]
    public void Part2()
    {
        var answer = _day02.Part2(_input!);
        Debug.WriteLine(answer);
        Assert.Equal(2101031224, answer);
    }
}
