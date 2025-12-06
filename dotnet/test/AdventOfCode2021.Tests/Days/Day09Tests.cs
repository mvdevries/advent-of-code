using System.IO;
using System.Threading.Tasks;
using Solutions.Days;
using Xunit;
using Xunit.Abstractions;

namespace Solutions.XTests.Days;

public class UnitTest09: IAsyncLifetime
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly Day09 _day09 = new();

    private string? _input;

    public UnitTest09(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    private const string _handInput = @"
2199943210
3987894921
9856789892
8767896789
9899965678
";

    public async Task InitializeAsync()
    {
        _input = await File.ReadAllTextAsync("./Inputs/Day09.txt");
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    [Fact]
    public void HandTestPart1()
    {
        var answer = _day09.Part1(_handInput);
        _testOutputHelper.WriteLine(answer.ToString());
    }

    [Fact]
    public void Part1()
    {
        var answer = _day09.Part1(_input!);
        _testOutputHelper.WriteLine(answer.ToString());
        Assert.Equal(535, answer);
    }

    [Fact]
    public void HandTestPart2()
    {
        var answer = _day09.Part2(_handInput);
        _testOutputHelper.WriteLine(answer.ToString());
    }

    [Fact]
    public void Part2()
    {
        var answer = _day09.Part2(_input!);
        _testOutputHelper.WriteLine(answer.ToString());
        Assert.Equal(1122700, answer);
    }
}
