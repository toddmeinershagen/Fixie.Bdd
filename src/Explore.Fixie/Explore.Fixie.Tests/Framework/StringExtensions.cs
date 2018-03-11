using System;

namespace Explore.Fixie.Tests.Framework
{
    public static class StringExtensions
    {
        public static bool IsGherkinStep(this string value)
        {
            var gherkinKeywords = new[] { "given", "when", "then" };

            if (value != null)
            {
                foreach (var keyword in gherkinKeywords)
                {
                    if (value.StartsWith(keyword, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}