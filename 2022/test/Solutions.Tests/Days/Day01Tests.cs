using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Solutions.Days;
using Xunit;

namespace Solutions.Tests.Days;

public class UnitTest01: IAsyncLifetime
{
    private readonly Day01 _day = new();
    private string? _input;

    private const string HandInput =
@"1000
2000
3000

4000

5000
6000

7000
8000
9000

10000
";

    public async Task InitializeAsync()
    {
        _input = await File.ReadAllTextAsync("./Inputs/Day01.txt");
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    [Fact]
    public void HandTestPart1()
    {
        var answer = _day.Part1(HandInput);
        Debug.WriteLine(answer);
    }

    [Fact]
    public void Part1()
    {
        var answer = _day.Part1(_input!);
        Debug.WriteLine(answer);
        Assert.Equal(70720, answer);
    }

    [Fact]
    public void HandTestPart2()
    {
        var answer = _day.Part2(HandInput);
        Debug.WriteLine(answer);
    }

    [Fact]
    public void Part2()
    {
        var answer = _day.Part2(_input!);
        Debug.WriteLine(answer);
        Assert.Equal(207148, answer);
    }
}
