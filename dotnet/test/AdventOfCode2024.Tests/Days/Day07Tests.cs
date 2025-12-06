using System.Diagnostics;
using Solutions.Days;

namespace Solutions.Tests.Days;

public class Day07Tests : IAsyncLifetime
{
    private readonly Day07 _day = new();
    private string _input = default!;
    private string _exampleInput = default!;
    
    public async Task InitializeAsync()
    {
        _input = await File.ReadAllTextAsync("./Inputs/Day07.txt");
        _exampleInput = await File.ReadAllTextAsync("./Inputs/Day07Example.txt");
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    [Fact]
    public void Part1Example()
    {
        var answer = _day.Part1(_exampleInput);
        Debug.WriteLine(answer);
        Assert.Equal(3749, answer);
    }
    
    [Fact]
    public void Part1()
    {
        var answer = _day.Part1(_input);
        Debug.WriteLine(answer);
        Assert.Equal(8401132154762, answer);
    }

    [Fact]
    public void Part2Example()
    {
        var answer = _day.Part2(_exampleInput);
        Debug.WriteLine(answer);
        Assert.Equal(11387, answer);
    }
    
    [Fact]
    public void Part2()
    {
        var answer = _day.Part2(_input);
        Debug.WriteLine(answer);
        Assert.Equal(95297119227552, answer);
    }
}