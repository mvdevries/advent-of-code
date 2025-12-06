using Solutions.Base;
using Solutions.Extensions;

namespace Solutions.Days;

public class Day06: IDay<long>
{
    private int Day(List<int> fishes, int fishesToSpawn)
    {
        var numberOfNewFishes = 0;

        for (var i = 0; i < fishes.Count; i++)
        {
            if (fishes[i] <= 0)
                fishes[i] = 6;
            else
                fishes[i]--;

            if (fishes[i] == 0) numberOfNewFishes++;
        }

        for (var j = 0; j < fishesToSpawn; j++)
        {
            fishes.Add(8);
        }

        return numberOfNewFishes;
    }

    public long Part1(string input)
    {
        var fishes = input.ToNumberList(",").ToList();
        var fishesToSpawn = 0;
        for (var i = 0; i < 80; i++)
        {
            fishesToSpawn = Day(fishes, fishesToSpawn);
        }

        return fishes.Count;
    }

    public long Part2(string input)
    {
        var fishes = input.ToNumberList(",").ToList();
        var fishTanks = new long[9];
        var newFishTanks = new long[9];

        foreach (var fish in fishes)
        {
            fishTanks[fish]++;
        }

        for (var i = 0; i < 256; i++)
        {
            long newFishSpawn = 0;
            if (fishTanks[0] > 0) {
                newFishSpawn = fishTanks[0];
            }

            for (var d = 0; d < 8; d++) {
                newFishTanks[d] = fishTanks[d + 1];
            }

            newFishTanks[6] += newFishSpawn;
            newFishTanks[8] += newFishSpawn;

            fishTanks = newFishTanks.ToArray();
            newFishTanks = new long[9];
        }

        return fishTanks.Sum();
    }
}
