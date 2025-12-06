using Solutions.Base;
using Solutions.Extensions;

namespace Solutions.Days;

public class Day06 : IDay<int>
{
    private int FindMarkerPosition(string input, int bufferSize)
    {
        for (var i = 0; i < input.Length - bufferSize; i++)
        {
            if (input.Substring(i, bufferSize).Distinct().Count() == bufferSize)
            {
                return i + bufferSize;
            }
        }

        return 0;
    }


    public int Part1(string input)
    {
        return FindMarkerPosition(input, 4);
    }

    public int Part2(string input)
    {
        return FindMarkerPosition(input, 14);
    }
}
