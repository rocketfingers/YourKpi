using System;

namespace YouKpiBackend.Extensions
{
    // compile with: -doc:DocFileName.xml
    public static class StringExtensions
    {
        /// <summary>Remove new lines in string and trim
        /// </summary>
        public static string RmNlTrim(this String oldString)
        {
            var newString = oldString.Replace("\n", "");
            newString = newString.Trim();

            return newString;
        }
    }
}
