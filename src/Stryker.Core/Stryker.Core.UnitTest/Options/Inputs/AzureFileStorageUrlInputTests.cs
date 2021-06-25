using Shouldly;
using Stryker.Core.Baseline.Providers;
using Stryker.Core.Exceptions;
using Stryker.Core.Options.Inputs;
using Xunit;

namespace Stryker.Core.UnitTest.Options.Inputs
{
    public class AzureFileStorageUrlInputTests
    {
        [Fact]
        public void ShouldHaveDefault()
        {
            var target = new AzureFileStorageUrlInput { SuppliedInput = null };

            var result = target.Validate(BaselineProvider.Dashboard);

            result.ShouldBe(string.Empty);
        }

        [Fact]
        public void ShouldAllowUri()
        {
            var target = new AzureFileStorageUrlInput { SuppliedInput = "http://example.com:8042" };

            var result = target.Validate(BaselineProvider.AzureFileStorage);

            result.ShouldBe("http://example.com:8042");
        }

        [Fact]
        public void ShouldThrowException_WhenAzureStorageUrlAndSASNull()
        {
            var target = new AzureFileStorageUrlInput { SuppliedInput = null };

            var exception = Should.Throw<InputException>(() => target.Validate(BaselineProvider.AzureFileStorage));

            exception.Message.ShouldBe(@"The azure file storage url is required when Azure File Storage is used for dashboard compare.");
        }

        [Fact]
        public void ShouldThrowException_OnInvalidUri()
        {
            var target = new AzureFileStorageUrlInput { SuppliedInput = "test" };

            var exception = Should.Throw<InputException>(() => target.Validate(BaselineProvider.AzureFileStorage));

            exception.Message.ShouldBe("The azure file storage url is not a valid Uri: test");
        }
    }
}