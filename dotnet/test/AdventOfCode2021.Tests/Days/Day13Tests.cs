using System.IO;
using System.Threading.Tasks;
using Solutions.Days;
using Xunit;
using Xunit.Abstractions;

namespace Solutions.XTests.Days;

public class UnitTest13: IAsyncLifetime
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly Day13 _day13 = new();

    private string? _input;

    public UnitTest13(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    private const string _handInput = @"
6,10
0,14
9,10
0,3
10,4
4,11
6,0
6,12
4,1
0,13
10,12
3,4
3,0
8,4
1,10
2,14
8,10
9,0

fold along y=7
fold along x=5
";

    public async Task InitializeAsync()
    {
        _input = await File.ReadAllTextAsync("./Inputs/Day13.txt");
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    [Fact]
    public void HandTestPart1()
    {
        var answer = _day13.Part1(_handInput);
        _testOutputHelper.WriteLine(answer.ToString());
    }

    [Fact]
    public void Part1()
    {
        var answer = _day13.Part1(_input!);
        _testOutputHelper.WriteLine(answer.ToString());
        Assert.Equal("712", answer);
    }

    [Fact]
    public void HandTestPart2()
    {
        var answer = _day13.Part2(_handInput);
        _testOutputHelper.WriteLine(answer.ToString());
    }

    [Fact]
    public void Part2()
    {
        var answer = _day13.Part2(_input!);
        _testOutputHelper.WriteLine(answer.ToString());
    }
}
