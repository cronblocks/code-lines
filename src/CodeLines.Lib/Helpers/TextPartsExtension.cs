namespace CodeLines.Lib.Helpers
{
    internal static class TextPartsExtension
    {
        internal static string GetTrimmedPartBefore(this string original, string ending)
        {
            if (!string.IsNullOrEmpty(original))
            {
                if (original.Contains(ending))
                {
                    return original.Substring(0, original.IndexOf(ending)).Trim();
                }
            }

            return original;
        }

        internal static string GetTrimmedPartAfter(this string original, string starting)
        {
            if (!string.IsNullOrEmpty(original))
            {
                if (original.Contains(starting))
                {
                    return original.Substring(original.IndexOf(starting) + starting.Length).Trim();
                }
            }

            return original;
        }

        internal static string GetTrimmedPartBetween(this string original, string start, string end)
        {
            if (!string.IsNullOrEmpty(original))
            {
                return original
                    .GetTrimmedPartAfter(start)
                    .GetTrimmedPartBefore(end);
            }

            return original;
        }
    }
}
