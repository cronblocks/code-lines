using CodeLines.Lib.Types;

namespace CodeLines.Lib.Helpers
{
    public static class LanguageToStringExtension
    {
        public static string StringName(this Language language)
        {
            switch (language)
            {
                case Language.ArduinoSketch:   return "Arduino Sketch";
                case Language.CPlusPlus:       return "C++";
                case Language.CSharp:          return "C#";
                case Language.GitIgnore:       return "Git Ignore";
                case Language.JavaScript:      return "Java Script";
                case Language.ObjectiveC:      return "Objective-C";
                case Language.PlainText:       return "Plain Text";
                case Language.ShellScript:     return "Shell-Script";
            }

            return language.ToString();
        }
    }
}
