using Solutions.Base;
using Solutions.Extensions;

namespace Solutions.Days;

public class Day01 : IDay<int>
{
    public int Part1(string input)
    {
        var lines = input.SplitOnLines(removeEmpty: true);
        var (list1, list2) = lines
            .Select(l => l.SplitTrimIn2Parts())
            .Aggregate((list1: new List<int>(), list2: new List<int>()), (acc, line) =>
            {
                acc.list1.Add(int.Parse(line.part1));
                acc.list2.Add(int.Parse(line.part2));
                return acc;
            });
        
        list1.Sort();
        list2.Sort();
        
        var resultsList = list1.Zip(list2).Select(pair => Math.Abs(pair.First - pair.Second)).ToList();
        return resultsList.Aggregate(0, (acc, result) => acc + result);
    }

    public int Part2(string input)
    {
        var lines = input.SplitOnLines(removeEmpty: true);
        var (list1, list2) = lines
            .Select(l => l.SplitTrimIn2Parts())
            .Aggregate((list1: new List<int>(), list2: new List<int>()), (acc, line) =>
            {
                acc.list1.Add(int.Parse(line.part1));
                acc.list2.Add(int.Parse(line.part2));
                return acc;
            });
        
        // Count amounts in list 2
        var list2Amounts = list2.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
        return list1.Aggregate(0, (acc, item) =>
        { 
            var hasItem = list2Amounts.TryGetValue(item, out var amount);
            return hasItem ? acc + (item * amount) : acc;
        });
    }
}