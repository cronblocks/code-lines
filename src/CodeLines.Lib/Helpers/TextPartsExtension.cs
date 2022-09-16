namespace CodeLines.Lib.Helpers
{
    internal static class TextPartsExtension
    {
        internal static string GetTrimmedPartBefore(this string original, string sep)
        {
            if (!string.IsNullOrEmpty(original))
            {
                if (original.Contains(sep))
                {
                    return original.Substring(0, original.IndexOf(sep)).Trim();
                }
            }

            return original;
        }
    }
}
