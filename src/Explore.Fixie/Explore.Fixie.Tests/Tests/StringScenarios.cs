using Explore.Fixie.Tests.Framework;
using FluentAssertions;

namespace Explore.Fixie.Tests.Tests
{
    public class StringScenarios
    {
        [Skip("Draft mode for method")]
        public void given_string_when_checking_if_starts_with_string_then_should_return_true()
        {
            var value = "Todd";
            value.StartsWith("T").Should().BeTrue();
        }
    }
}