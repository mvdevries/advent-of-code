using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solutions.Days;

namespace Solutions.Tests.Days;

[TestClass]
public class UnitTest2
{
    private readonly Day02 _day02 = new();
    private string _input;

    [TestInitialize]
    public async Task Initialize()
    {
        _input = await File.ReadAllTextAsync("./Inputs/Day02.txt");
    }

    [TestMethod]
    public void HandTestPart1()
    {
        var input = @"forward 5
down 5
forward 8
up 3
down 8
forward 2";
        var answer = _day02.Part1(input);
        Debug.WriteLine(answer);
    }

    [TestMethod]
    public void Part1()
    {
        var answer = _day02.Part1(_input);
        Debug.WriteLine(answer);
        Assert.AreEqual(answer, 2102357);
    }

    [TestMethod]
    public void HandTestPart2()
    {
        var input = @"forward 5
down 5
forward 8
up 3
down 8
forward 2";
        var answer = _day02.Part2(input);
        Debug.WriteLine(answer);
    }

    [TestMethod]
    public void Part2()
    {
        var answer = _day02.Part2(_input);
        Debug.WriteLine(answer);
        Assert.AreEqual(answer, 2101031224);
    }
}