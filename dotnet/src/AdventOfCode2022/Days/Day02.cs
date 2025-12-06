using Solutions.Base;
using Solutions.Extensions;

namespace Solutions.Days;

public class Day02: IDay<int>
{
    private List<Turn> ParseInputPart1(string input)
    {
        return input.ToStringList().Select(turn =>
        {
            var splitTurns = turn.Split(" ");
            return new Turn(splitTurns[0].ToRockPaperScissors(), splitTurns[1].ToRockPaperScissors());
        }).ToList();
    }

    private List<TurnWin> ParseInputPart2(string input)
    {
        return input.ToStringList().Select(turn =>
        {
            var splitTurns = turn.Split(" ");
            return new TurnWin(
                splitTurns[0].ToRockPaperScissors().NeedToEndIn(splitTurns[1].ToOutcome()),
                splitTurns[1].ToOutcome()
            );
        }).ToList();
    }

    public int Part1(string input)
    {
        var turns = ParseInputPart1(input);
        return turns.Select(turn => (int) turn.me + (int) turn.me.DidWin(turn.other))
            .Aggregate(0, (acc, score) => acc + score);
    }

    public int Part2(string input)
    {
        var turns = ParseInputPart2(input);
        return turns.Select(turn => (int) turn.me + (int) turn.outcome)
            .Aggregate(0, (acc, score) => acc + score);
    }
}

record TurnWin(RockPaperScissors me, Outcome outcome);

record Turn(RockPaperScissors other, RockPaperScissors me);

public enum RockPaperScissors
{
    Rock = 1,
    Paper = 2,
    Sissors = 3,
}

public enum Outcome
{
    Win = 6,
    Lose = 0,
    Draw = 3,
}

public static class RockPaperScissorsHelper
{
    public static RockPaperScissors NeedToEndIn(this RockPaperScissors hand, Outcome outcome)
    {
        return HandNeedsToEndIn(hand, outcome);
    }

    public static RockPaperScissors HandNeedsToEndIn(RockPaperScissors hand, Outcome outcome)
    {
        if (outcome == Outcome.Win)
        {
            if (hand == RockPaperScissors.Rock)
            {
                return RockPaperScissors.Paper;
            }

            if (hand == RockPaperScissors.Paper)
            {
                return RockPaperScissors.Sissors;
            }

            return RockPaperScissors.Rock;
        }

        if (outcome == Outcome.Lose)
        {
            if (hand == RockPaperScissors.Rock)
            {
                return RockPaperScissors.Sissors;
            }

            if (hand == RockPaperScissors.Paper)
            {
                return RockPaperScissors.Rock;
            }

            return RockPaperScissors.Paper;
        }

        return hand;
    }

    private static Outcome CheckWinMeOther(RockPaperScissors me, RockPaperScissors other)
    {
        if (me == RockPaperScissors.Rock && other == RockPaperScissors.Sissors ||
            me == RockPaperScissors.Paper && other == RockPaperScissors.Rock ||
            me == RockPaperScissors.Sissors && other == RockPaperScissors.Paper)
        {
            return Outcome.Win;
        }

        if (me == RockPaperScissors.Rock && other == RockPaperScissors.Paper ||
            me == RockPaperScissors.Paper && other == RockPaperScissors.Sissors ||
            me == RockPaperScissors.Sissors && other == RockPaperScissors.Rock)
        {
            return Outcome.Lose;
        }

        return Outcome.Draw;
    }

    public static Outcome DidWin(this RockPaperScissors me, RockPaperScissors other)
    {
        return CheckWinMeOther(me, other);
    }

    public static RockPaperScissors ToRockPaperScissors(this string letter) => StringToRockPaperScissors(letter);

    private static RockPaperScissors StringToRockPaperScissors(string letter) => letter switch
    {
        "A" => RockPaperScissors.Rock,
        "B" => RockPaperScissors.Paper,
        "C" => RockPaperScissors.Sissors,
        "X" => RockPaperScissors.Rock,
        "Y" => RockPaperScissors.Paper,
        "Z" => RockPaperScissors.Sissors,
    };

    public static Outcome ToOutcome(this string letter) => StringToOutcome(letter);

    private static Outcome StringToOutcome(string letter) => letter switch
    {
        "X" => Outcome.Lose,
        "Y" => Outcome.Draw,
        "Z" => Outcome.Win,
    };
}
