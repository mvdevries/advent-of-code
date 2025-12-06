using Solutions.Base;
using Solutions.Extensions;

namespace Solutions.Days;

public class Day08: IDay<int>
{
    private class Display
    {
        public string[] Input { get; }
        public string[] Output { get; }
        public int OutputNumber { get; }

        public Display(string[] input, string[] output)
        {
            Input = input.Select(i => new string(i.OrderBy(c => c).ToArray())).ToArray();
            Output = output.Select(i => new string(i.OrderBy(c => c).ToArray())).ToArray();

            Dictionary<string, int> segmentNumberMap = new();
            Dictionary<int, string> numberSegmentMap = new();

            var segments1 = Input.First(s => s.Length == 2);
            segmentNumberMap.Add(segments1, 1);
            numberSegmentMap.Add(1, segments1);

            var segments4 = Input.First(s => s.Length == 4);
            segmentNumberMap.Add(segments4, 4);
            numberSegmentMap.Add(4, segments4);

            var segments7 = Input.First(s => s.Length == 3);
            segmentNumberMap.Add(segments7, 7);
            numberSegmentMap.Add(7, segments7);

            var segments8 = Input.First(s => s.Length == 7);
            segmentNumberMap.Add(segments8, 8);
            numberSegmentMap.Add(8, segments8);

            // 9 0 6
            // 2 3 5

            var segments9 = Input.First(s => s.Length == 6 && numberSegmentMap[4].All(s.Contains));
            segmentNumberMap.Add(segments9, 9);
            numberSegmentMap.Add(9, segments9);

            var segment3 = Input.First(s => s.Length == 5 && numberSegmentMap[1].All(s.Contains));
            segmentNumberMap.Add(segment3, 3);
            numberSegmentMap.Add(3, segment3);

            var segments0 = Input.First(s => s.Length == 6 && numberSegmentMap[1].All(s.Contains) && s != numberSegmentMap[9]);
            segmentNumberMap.Add(segments0, 0);
            numberSegmentMap.Add(0, segments0);

            var segments6 = Input.First(s => s.Length == 6 && numberSegmentMap[0] != s && numberSegmentMap[9] != s);
            segmentNumberMap.Add(segments6, 6);
            numberSegmentMap.Add(6, segments6);

            var segment5 = Input.First(s => s.Length == 5 && numberSegmentMap[6].Except(s).Count() == 1);
            segmentNumberMap.Add(segment5, 5);
            numberSegmentMap.Add(5, segment5);

            var segment2 = Input.First(s => s.Length == 5 && s != numberSegmentMap[3] && s != numberSegmentMap[5]);
            segmentNumberMap.Add(segment2, 2);
            numberSegmentMap.Add(2, segment2);

            OutputNumber = segmentNumberMap[Output[0]] * 1000 +
                           segmentNumberMap[Output[1]] * 100 +
                           segmentNumberMap[Output[2]] * 10 +
                           segmentNumberMap[Output[3]];
        }
    }

    private IEnumerable<Display> ParseInput(string input)
    {
        return input.ToStringList().Select(line =>
        {
            var displayParts = line.Split(' ');
            return new Display(displayParts[..10], displayParts[11..]);
        });
    }

    public int Part1(string input)
    {
        var displays = ParseInput(input).ToArray();
        return displays.Select(d => d.Output.Count(ds => ds.Length is 2 or 4 or 3 or 7)).Sum();
    }

    public int Part2(string input)
    {
        var displays = ParseInput(input).ToArray();
        return displays.Sum(d => d.OutputNumber);
    }
}
