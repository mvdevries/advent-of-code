using System;

namespace Solutions.Base;

interface IDay<Out> where Out: IComparable
{
    Out Part1(string input);

    Out Part2(string input);
}
