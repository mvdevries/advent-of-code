using System.IO;
using System.Threading.Tasks;
using Solutions.Days;
using Xunit;
using Xunit.Abstractions;

namespace Solutions.XTests.Days;

public class UnitTest11: IAsyncLifetime
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly Day11 _day11 = new();

    private string? _input;

    public UnitTest11(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    private const string _handInput = @"
5483143223
2745854711
5264556173
6141336146
6357385478
4167524645
2176841721
6882881134
4846848554
5283751526
";

    public async Task InitializeAsync()
    {
        _input = await File.ReadAllTextAsync("./Inputs/Day11.txt");
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    [Fact]
    public void HandTestPart1()
    {
        var answer = _day11.Part1(_handInput);
        _testOutputHelper.WriteLine(answer.ToString());
    }

    [Fact]
    public void Part1()
    {
        var answer = _day11.Part1(_input!);
        _testOutputHelper.WriteLine(answer.ToString());
        Assert.Equal(1562, answer);
    }

    [Fact]
    public void HandTestPart2()
    {
        var answer = _day11.Part2(_handInput);
        _testOutputHelper.WriteLine(answer.ToString());
    }

    [Fact]
    public void Part2()
    {
        var answer = _day11.Part2(_input!);
        _testOutputHelper.WriteLine(answer.ToString());
        Assert.Equal(268, answer);
    }
}
