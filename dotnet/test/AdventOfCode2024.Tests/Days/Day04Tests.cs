using System.Diagnostics;
using Solutions.Days;

namespace Solutions.Tests.Days;

public class Day04Tests : IAsyncLifetime
{
    private readonly Day04 _day = new();
    private string _input = default!;
    private string _exampleInput = default!;
    
    public async Task InitializeAsync()
    {
        _input = await File.ReadAllTextAsync("./Inputs/Day04.txt");
        _exampleInput = await File.ReadAllTextAsync("./Inputs/Day04Example.txt");
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
        Assert.Equal(18, answer);
    }
    
    [Fact]
    public void Part1()
    {
        var answer = _day.Part1(_input);
        Debug.WriteLine(answer);
        Assert.Equal(2358, answer);
    }

    [Fact]
    public void Part2Example()
    {
        var answer = _day.Part2(_exampleInput);
        Debug.WriteLine(answer);
        Assert.Equal(9, answer);
    }
    
    [Fact]
    public void Part2()
    {
        var answer = _day.Part2(_input);
        Debug.WriteLine(answer);
        Assert.Equal(1737, answer);
    }
}