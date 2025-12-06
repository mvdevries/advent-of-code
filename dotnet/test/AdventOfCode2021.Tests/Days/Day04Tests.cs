using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Solutions.Days;
using Xunit;

namespace Solutions.XTests.Days;

public class UnitTest04: IAsyncLifetime
{
    private readonly Day04 _day04 = new();
    private string? _input;
    private const string _handInput = @"
7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1

22 13 17 11  0
 8  2 23  4 24
21  9 14 16  7
 6 10  3 18  5
 1 12 20 15 19

 3 15  0  2 22
 9 18 13 17  5
19  8  7 25 23
20 11 10 24  4
14 21 16 12  6

14 21 17 24  4
10 16 15  9 19
18  8 23 26 20
22 11 13  6  5
 2  0 12  3  7
";

    public async Task InitializeAsync()
    {
        _input = await File.ReadAllTextAsync("./Inputs/Day04.txt");
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    [Fact]
    public void HandTestPart1()
    {
        var answer = _day04.Part1(_handInput);
        Debug.WriteLine(answer);
    }

    [Fact]
    public void Part1()
    {
        var answer = _day04.Part1(_input!);
        Debug.WriteLine(answer);
        Assert.Equal(5685, answer);
    }

    [Fact]
    public void HandTestPart2()
    {
        var answer = _day04.Part2(_handInput);
        Debug.WriteLine(answer);
    }

    [Fact]
    public void Part2()
    {
        var answer = _day04.Part2(_input!);
        Debug.WriteLine(answer);
        Assert.Equal(21070, answer);
    }
}
