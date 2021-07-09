using Shouldly;
using Stryker.Core.Exceptions;
using Stryker.Core.Options;
using Stryker.Core.Options.Inputs;
using Xunit;

namespace Stryker.Core.UnitTest.Options.Inputs
{
    public class OptimizationModeInputTests
    {
        [Fact]
        public void ShouldHaveHelptext()
        {
            var target = new CoverageAnalysisInput();
            target.HelpText.ShouldBe(@"Use coverage info to speed up execution. Possible values are: off, all, perTest, perIsolatedTest.
    - off: coverage data is not captured.
    - perTest (Default): capture the list of mutations covered by each test. For every mutation that has tests, only the tests that cover this mutation are tested. Fastest option.
    - all: capture the list of mutations covered by each test. Test only these mutations. Fast option.
    - perTestInIsolation: like 'perTest', but running each test in an isolated run. Slowest fast option. | default: 'perTest'");
        }

        [Theory]
        [InlineData(null, OptimizationModes.CaptureCoveragePerTest)]
        [InlineData("pertestinisolation", OptimizationModes.CoverageBasedTest, OptimizationModes.CaptureCoveragePerTest)]
        [InlineData("pertest", OptimizationModes.CoverageBasedTest)]
        [InlineData("all", OptimizationModes.SkipUncoveredMutants)]
        [InlineData("off", OptimizationModes.NoOptimization)]
        public void ShouldSetFlags(string value, params OptimizationModes[] expectedFlags)
        {
            var target = new CoverageAnalysisInput { SuppliedInput = value };

            var result = target.Validate();

            foreach (var flag in expectedFlags)
            {
                result.HasFlag(flag).ShouldBeTrue();
            }
        }

        [Fact]
        public void ShouldThrowOnInvalidOptimizationMode()
        {
            var target = new CoverageAnalysisInput { SuppliedInput = "gibberish" };

            var ex = Assert.Throws<InputException>(() => target.Validate());

            ex.Message.ShouldBe($"Incorrect coverageAnalysis option (gibberish). The options are [Off, All, PerTest or PerTestInIsolation].");
        }
    }
}
