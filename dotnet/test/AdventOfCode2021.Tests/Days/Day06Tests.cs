using System.IO;
using System.Threading.Tasks;
using Solutions.Days;
using Xunit;
using Xunit.Abstractions;

namespace Solutions.XTests.Days;

public class UnitTest06: IAsyncLifetime
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly Day06 _day06 = new();

    private string? _input;

    public UnitTest06(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    private const string _handInput = @"3,4,3,1,2";

    public async Task InitializeAsync()
    {
        _input = await File.ReadAllTextAsync("./Inputs/Day06.txt");
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    [Fact]
    public void HandTestPart1()
    {
        var answer = _day06.Part1(_handInput);
        _testOutputHelper.WriteLine(answer.ToString());
    }

    [Fact]
    public void Part1()
    {
        var answer = _day06.Part1(_input!);
        _testOutputHelper.WriteLine(answer.ToString());
        Assert.Equal(374927, answer);
    }

    [Fact]
    public void HandTestPart2()
    {
        var answer = _day06.Part2(_handInput);
        _testOutputHelper.WriteLine(answer.ToString());
    }

    [Fact]
    public void Part2()
    {
        var answer = _day06.Part2(_input!);
        _testOutputHelper.WriteLine(answer.ToString());
        Assert.Equal(1687617803407, answer);
    }
}
