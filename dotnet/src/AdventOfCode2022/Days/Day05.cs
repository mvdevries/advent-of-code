using System.Text.RegularExpressions;
using Solutions.Base;
using Solutions.Extensions;

namespace Solutions.Days;

public class Day05 : IDay<string>
{
    private Stack<char>[] ParseStacks(string input)
    {
        var lines = input.ToStringList().Reverse().ToList();
        var stackCount = lines.First().Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
        var stacks = Enumerable.Range(0, stackCount).Select((_, _) => new Stack<char>()).ToArray();
        for (var i = 1; i < lines.Count; i++)
        {
            var containers = Regex.Matches(lines[i], @"\[[\w ]{1}\]");
            for (var j = 0; j < containers.Count; j++)
            {
                var letter = containers[j].Value[1];
                if (!char.IsWhiteSpace(letter))
                {
                    stacks[j].Push(letter);
                }
            }
        }

        return stacks;
    }

    private List<Instruction> ParseInstructions(string input)
    {
        return input.ToStringList().Select(l =>
        {
            var splitLine = l.Split(' ');
            return new Instruction(int.Parse(splitLine[1]), int.Parse(splitLine[3]), int.Parse(splitLine[5]));
        }).ToList();
    }

    private ParsedInput ParseInput(string input)
    {
        var splitInput = input.Split($"{Environment.NewLine}{Environment.NewLine}");
        var stacks = ParseStacks(splitInput[0]);
        var instructions = ParseInstructions(splitInput[1]);
        return new ParsedInput(stacks, instructions);
    }

    public string Part1(string input)
    {
        var (stacks, instructions) = ParseInput(input);

        foreach (var instruction in instructions)
        {
            for (var i = 0; i < instruction.Count; i++)
            {
                if (stacks[instruction.From - 1].TryPop(out var container))
                {
                    stacks[instruction.To - 1].Push(container);
                }
            }
        }

        return new string(stacks.Select(stack => stack.Peek()).ToArray());
    }

    public string Part2(string input)
    {
        var (stacks, instructions) = ParseInput(input);

        foreach (var instruction in instructions)
        {
            var containers = stacks[instruction.From - 1].Take(instruction.Count).Reverse().ToList();

            foreach (var t in containers)
            {
                stacks[instruction.From - 1].Pop();
                stacks[instruction.To - 1].Push(t);
            }
        }

        return new string(stacks.Select(stack => stack.Peek()).ToArray());
    }
}

record ParsedInput(Stack<char>[] Stacks, List<Instruction> Instructions);

record Instruction(int Count, int From, int To);
