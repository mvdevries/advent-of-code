using System.Diagnostics;
using Solutions.Days;

namespace Solutions.Tests.Days;

public class Day03Tests : IAsyncLifetime
{
    private readonly Day03 _day = new();
    private string _input = default!;
    private string _exampleInput = default!;
    private string _exampleInput2 = default!;
    
    public async Task InitializeAsync()
    {
        _input = await File.ReadAllTextAsync("./Inputs/Day03.txt");
        _exampleInput = await File.ReadAllTextAsync("./Inputs/Day03Example.txt");
        _exampleInput2 = await File.ReadAllTextAsync("./Inputs/Day03Example2.txt");
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
        Assert.Equal(161, answer);
    }
    
    [Fact]
    public void Part1()
    {
        var answer = _day.Part1(_input);
        Debug.WriteLine(answer);
        Assert.Equal(162813399, answer);
    }

    [Fact]
    public void Part2Example()
    {
        var answer = _day.Part2(_exampleInput2);
        Debug.WriteLine(answer);
        Assert.Equal(8, answer);
    }
    
    [Fact]
    public void Part2()
    {
        var answer = _day.Part2(_input);
        Debug.WriteLine(answer);
        Assert.Equal(53783319, answer);
    }
}