namespace AdventOfCode2025.Base;

public interface IDay<TOut> where TOut: IComparable
{
    TOut Part1(string input);

    TOut Part2(string input);
}
