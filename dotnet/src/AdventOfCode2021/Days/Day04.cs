using System.Drawing;
using Solutions.Base;
using Solutions.Extensions;

namespace Solutions.Days;

public class Day04: IDay<int>
{
    private class BingoCard
    {
        public Dictionary<int, Point> Numbers { get; }
        public bool[,] Checked { get; } = new bool[5, 5];

        public BingoCard(Dictionary<int, Point> numbers)
        {
            Numbers = numbers;
        }

        public int UnmatchedSum()
        {
            return Numbers
                .Where(kvp => !Checked[kvp.Value.X, kvp.Value.Y])
                .Sum(kvp => kvp.Key);
        }

        public bool IsBingo
        {
            get
            {
                for (var x = 0; x < 5; x++)
                {
                    if (Enumerable.Range(0, 5).All(y => Checked[x, y]))
                        return true;
                }

                for (var y = 0; y < 5; y++)
                {
                    if (Enumerable.Range(0, 5).All(x => Checked[x, y]))
                        return true;
                }

                return false;
            }
        }
    }

    private (int[], List<BingoCard>) ParseInput(string input)
    {
        var lines = input
            .ToStringList("\n", StringSplitOptions.RemoveEmptyEntries)
            .ToArray();

        var calledNumbers = lines[0]
            .ToNumberList(",")
            .ToArray();

        var bingoCards = lines[1..]
            .Batch(5)
            .Select(b =>
            {
                return b
                    .SelectMany((l, x) =>
                    {
                        return l
                            .Split()
                            .Where(s => s.Length > 0)
                            .Select((s, y) => (key: Convert.ToInt32(s), value: new Point(x, y)));
                    })
                    .Aggregate(new Dictionary<int, Point>(), (acc, p) =>
                    {
                        acc.Add(p.key, p.value);
                        return acc;
                    });
            })
            .Select(d => new BingoCard(d))
            .ToList();

        return (calledNumbers, bingoCards);
    }

    public int Part1(string input)
    {
        var (calledNumbers, bingoCards) = ParseInput(input);

        foreach (var calledNumber in calledNumbers)
        {
            foreach (var bingoCard in bingoCards)
            {
                var location = bingoCard.Numbers.GetValueOrDefault(calledNumber);
                bingoCard.Checked[location.X, location.Y] = true;

                if (bingoCard.IsBingo)
                {
                    return calledNumber * bingoCard.UnmatchedSum();
                }
            }
        }

        return default;
    }

    public int Part2(string input)
    {
        var (calledNumbers, bingoCards) = ParseInput(input);

        foreach (var calledNumber in calledNumbers)
        {
            foreach (var bingoCard in bingoCards)
            {
                var location = bingoCard.Numbers.GetValueOrDefault(calledNumber);
                bingoCard.Checked[location.X, location.Y] = true;

                if (bingoCards.Count == 1 && bingoCards.First().IsBingo)
                {
                    return calledNumber * bingoCard.UnmatchedSum();
                }
            }

            bingoCards.RemoveAll(card => card.IsBingo);
        }

        return default;
    }
}
