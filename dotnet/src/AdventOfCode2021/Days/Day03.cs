using Solutions.Base;
using Solutions.Extensions;

namespace Solutions.Days;

public class Day03: IDay<int>
{
    public int Part1(string input)
    {
        var rows = input.ToStringList().ToArray();
        var rowCount = 0;

        var columns = new int[rows[0].Length];

        foreach (var row in rows)
        {
            rowCount++;

            foreach (var (item, index) in row.Select((item, index) => (item, index)))
            {
                columns[index] += item == '1' ? 1 : 0;
            }
        }

        var binary = "";
        var half = rowCount / 2;

        foreach (var column in columns)
        {
            binary += column > half ? 1 : 0;
        }

        var gammaRate = Convert.ToInt32(binary, 2);
        var epsilonRate = (int)(Math.Pow(2, columns.Length) - 1 - gammaRate);
        return gammaRate * epsilonRate;
    }

    public int Part2(string input)
    {
        var rows = input.ToStringList().ToList();

        var oxygenRate = rows.ToList();
        var co2Scrubber = rows.ToList();
        var count = oxygenRate.Count;

        for (var i = 0; i < count; i++)
        {
            if (oxygenRate.Count != 1)
            {
                var zeroCounter = 0;
                var oneCounter = 0;

                foreach (var str in oxygenRate)
                {
                    if (str[i] == '0')
                        zeroCounter++;
                    else
                        oneCounter++;
                }
                var oType = (zeroCounter == oneCounter || oneCounter > zeroCounter) ? '1' : '0';
                foreach (var str in oxygenRate.ToList().Where(str => str[i] != oType))
                {
                    oxygenRate.Remove(str);
                }
            }

            if (co2Scrubber.Count != 1)
            {
                var zeroCounter = 0;
                var oneCounter = 0;
                foreach (var str in co2Scrubber)
                {
                    if (str[i] == '0')
                        zeroCounter++;
                    else
                        oneCounter++;
                }

                var cType = (zeroCounter == oneCounter || oneCounter > zeroCounter) ? '0' : '1';
                foreach (var str in co2Scrubber.ToList().Where(str => str[i] != cType))
                {
                    co2Scrubber.Remove(str);
                }
            }

            if (oxygenRate.Count == 1 && co2Scrubber.Count == 1)
                break;
        }

        return Convert.ToInt32(oxygenRate[0], 2) * Convert.ToInt32(co2Scrubber[0], 2);
    }
}
