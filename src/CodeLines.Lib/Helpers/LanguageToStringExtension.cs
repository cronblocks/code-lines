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
            switch (language)
            {
                case Language.CPlusPlus:     return "C++";
                case Language.CSharp:        return "C#";
                case Language.JavaScript:    return "Java Script";
                case Language.ObjectiveC:    return "Objective-C";
                case Language.ShellScript:   return "Shell-Script";
            }

            return language.ToString();
        }
    }
}
