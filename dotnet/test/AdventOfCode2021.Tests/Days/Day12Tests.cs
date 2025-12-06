using System.IO;
using System.Threading.Tasks;
using Solutions.Days;
using Xunit;
using Xunit.Abstractions;

namespace Solutions.XTests.Days;

public class UnitTest12: IAsyncLifetime
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly Day12 _day12 = new();

    private string? _input;

    public UnitTest12(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    private const string _handInput = @"
dc-end
HN-start
start-kj
dc-start
dc-HN
LN-dc
HN-end
kj-sa
kj-HN
kj-dc
";

    public async Task InitializeAsync()
    {
        _input = await File.ReadAllTextAsync("./Inputs/Day12.txt");
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    [Fact]
    public void HandTestPart1()
    {
        var answer = _day12.Part1(_handInput);
        _testOutputHelper.WriteLine(answer.ToString());
    }

    [Fact]
    public void Part1()
    {
        var answer = _day12.Part1(_input!);
        _testOutputHelper.WriteLine(answer.ToString());
        Assert.Equal(4970, answer);
    }

    [Fact]
    public void HandTestPart2()
    {
        var answer = _day12.Part2(_handInput);
        _testOutputHelper.WriteLine(answer.ToString());
    }

    [Fact]
    public void Part2()
    {
        var answer = _day12.Part2(_input!);
        _testOutputHelper.WriteLine(answer.ToString());
        Assert.Equal(137948, answer);
    }
}
