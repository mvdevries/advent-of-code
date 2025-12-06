using System.IO;
using System.Threading.Tasks;
using Solutions.Days;
using Xunit;
using Xunit.Abstractions;

namespace Solutions.XTests.Days;

public class UnitTest07: IAsyncLifetime
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly Day07 _day07 = new();

    private string? _input;

    public UnitTest07(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    private const string _handInput = @"16,1,2,0,4,2,7,1,2,14";

    public async Task InitializeAsync()
    {
        _input = await File.ReadAllTextAsync("./Inputs/Day07.txt");
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    [Fact]
    public void HandTestPart1()
    {
        var answer = _day07.Part1(_handInput);
        _testOutputHelper.WriteLine(answer.ToString());
    }

    [Fact]
    public void Part1()
    {
        var answer = _day07.Part1(_input!);
        _testOutputHelper.WriteLine(answer.ToString());
        Assert.Equal(356922, answer);
    }

    [Fact]
    public void HandTestPart2()
    {
        var answer = _day07.Part2(_handInput);
        _testOutputHelper.WriteLine(answer.ToString());
    }

    [Fact]
    public void Part2()
    {
        var answer = _day07.Part2(_input!);
        _testOutputHelper.WriteLine(answer.ToString());
        Assert.Equal(100347031, answer);
    }
}
