using System.IO;
using System.Threading.Tasks;
using Solutions.Days;
using Xunit;
using Xunit.Abstractions;

namespace Solutions.XTests.Days;

public class UnitTest05: IAsyncLifetime
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly Day05 _day05 = new();

    private string? _input;

    public UnitTest05(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    private const string _handInput = @"
0,9 -> 5,9
8,0 -> 0,8
9,4 -> 3,4
2,2 -> 2,1
7,0 -> 7,4
6,4 -> 2,0
0,9 -> 2,9
3,4 -> 1,4
0,0 -> 8,8
5,5 -> 8,2";

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
        var answer = _day05.Part1(_handInput);
        _testOutputHelper.WriteLine(answer.ToString());
    }

    [Fact]
    public void Part1()
    {
        var answer = _day05.Part1(_input!);
        _testOutputHelper.WriteLine(answer.ToString());
        Assert.Equal(5147, answer);
    }

    [Fact]
    public void HandTestPart2()
    {
        var answer = _day05.Part2(_handInput);
        _testOutputHelper.WriteLine(answer.ToString());
    }

    [Fact]
    public void Part2()
    {
        var answer = _day05.Part2(_input!);
        _testOutputHelper.WriteLine(answer.ToString());
        Assert.Equal(16925, answer);
    }
}
