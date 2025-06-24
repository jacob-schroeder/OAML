namespace Common.Extensions;

public static class CliExtensions
{
    public static IEnumerable<string> Tokenize(this string input)
    {
        var matches = System.Text.RegularExpressions.Regex.Matches(input, @"[\""].+?[\""]|\S+");
        var tokens = new List<string>();
        foreach (System.Text.RegularExpressions.Match match in matches)
        {
            string val = match.Value;
            if (val.StartsWith("\"") && val.EndsWith("\""))
                val = val[1..^1];
            tokens.Add(val);
        }
        return tokens;
    }

}