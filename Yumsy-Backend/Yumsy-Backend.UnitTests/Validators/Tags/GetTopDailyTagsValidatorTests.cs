using FluentValidation.TestHelper;
using Yumsy_Backend.Features.Tags.GetTopDailyTags;

namespace Yumsy_Backend.UnitTests.Validators.Tags;

public class GetTopDailyTagsValidatorTests
{
    private readonly GetTopDailyTagsValidator _validator = new();

    [Fact]
    public void Should_NotHaveError_ForAnyRequest()
    {
        var request = new GetTopDailyTagsRequest();
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
