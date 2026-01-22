using FluentValidation.TestHelper;
using Yumsy_Backend.Features.Tags.GetTags;

namespace Yumsy_Backend.UnitTests.Validators.Tags;

public class GetTagsValidatorTests
{
    private readonly GetTagsValidator _validator = new();

    [Fact]
    public void Should_NotHaveError_ForAnyRequest()
    {
        var request = new GetTagsRequest();
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
