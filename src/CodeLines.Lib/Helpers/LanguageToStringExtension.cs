using CodeLines.Lib.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLines.Lib.Helpers
{
    public static class LanguageToStringExtension
    {
        public static string StringName(this Language language)
        {
            return language.ToString();
        }
    }
}
