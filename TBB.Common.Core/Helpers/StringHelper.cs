namespace TBB.Common.Core.Helpers;

public static class StringHelper
{
    public static string Trim(string s)
    {
        var result = string.Empty;
        var prevIsSpace = false;

        foreach (var c in s)
        {
            if (c != ' ')
            {
                result += c;
                prevIsSpace = false;
            }
            else if (!prevIsSpace)
            {
                result += ' ';
                prevIsSpace = true;
            }
        }

        return result.Trim();
    }
}