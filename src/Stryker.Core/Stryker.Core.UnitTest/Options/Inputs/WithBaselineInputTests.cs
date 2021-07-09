using Shouldly;
using Stryker.Core.Options.Inputs;
using Xunit;

namespace Stryker.Core.UnitTest.Options.Inputs
{
    public class WithBaselineInputTests
    {
        [Fact]
        public void ShouldHaveHelptext()
        {
            var target = new WithBaselineInput();
            target.HelpText.ShouldBe(@"EXPERIMENTAL: Use results stored in stryker dashboard to only test new mutants. | default: 'False'");
        }

        [Fact]
        public void ShouldBeEnabledWhenTrue()
        {
            var target = new WithBaselineInput { SuppliedInput = true };

            var result = target.Validate();

            result.ShouldBeTrue();
        }

        [Fact]
        public void ShouldProvideDefaultWhenNull()
        {
            var target = new WithBaselineInput { SuppliedInput = null };

            var result = target.Validate();

            result.ShouldBe(target.Default.Value);
        }

        [Fact]
        public void ShouldNotBeEnabledWhenFalse()
        {
            var target = new WithBaselineInput { SuppliedInput = false };

            var result = target.Validate();

            result.ShouldBeFalse();
        }
    }
}
