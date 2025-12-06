namespace Solutions.Extensions;

public static class InputExtensions
{
    public static IEnumerable<string[]> ToPartsList(this string input, string seperator = "\n")
    {
        return StringToPartsList(input, seperator);
    }

    private static IEnumerable<string[]> StringToPartsList(string input, string seperator = "\n")
    {
        return input.Trim().Split(seperator).Select(l => l.Trim().Split(' '));
    }

    public static IEnumerable<int> ToNumberList(this string input, string seperator = "\n")
    {
        return StringToNumberList(input, seperator);
    }

    private static IEnumerable<int> StringToNumberList(string input, string seperator = "\n")
    {
        return input.Trim().Split(seperator).Select(e => int.Parse(e.Trim()));
    }

    public static IEnumerable<string> ToStringList(this string input, string seperator = "\n", StringSplitOptions options = StringSplitOptions.None)
    {
        return StringToStringList(input, seperator, options);
    }

    private static IEnumerable<string> StringToStringList(string input, string seperator = "\n", StringSplitOptions options = StringSplitOptions.None)
    {
        return input.Trim().Split(seperator, options);
    }
}
