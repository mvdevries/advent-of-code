using System.Collections;
using System.Text.RegularExpressions;

namespace Solutions.Extensions;

public static partial class StringExtensions
{
    public static string RemoveWhitespace(this string input)
    {
        return string.IsNullOrEmpty(input) ? input : WhitespaceRegex().Replace(input, string.Empty);
    }
    
    public static IEnumerable<string> SplitOnLines(this string input, string seperator = "\n",
        bool removeEmpty = true, StringSplitOptions options = StringSplitOptions.None)
    {
        return removeEmpty
            ? input.Split(seperator, options).Where(line => !string.IsNullOrWhiteSpace(line))
            : input.Split(seperator, options);
    }

    public static (string part1, string part2) SplitTrimIn2Parts(this string input,
        string separator = " ", StringSplitOptions options = StringSplitOptions.None)
    {
        var parts = input
            .Split(separator, options)
            .Where(part => !string.IsNullOrWhiteSpace(part))
            .Select(part => part.Trim())
            .ToArray();

        return (parts[0], parts[1]);
    }
    
    public static IEnumerable<string> SplitOnSpace(this string input, bool removeEmpty = true)
    {
        return removeEmpty
            ? input.Split(' ').Where(line => !string.IsNullOrWhiteSpace(line))
            : input.Split(' ');
    }

    [GeneratedRegex(@"\s+")]
    private static partial Regex WhitespaceRegex();
}