using FluentValidation.TestHelper;
using Yumsy_Backend.Features.ShoppingLists.DeleteShoppingList;

namespace Yumsy_Backend.UnitTests.Validators.ShoppingLists;

public class DeleteShoppingListValidatorTests
{
    private readonly DeleteShoppingListValidator _validator = new();

    [Fact]
    public void Should_HaveError_When_ShoppingListIdIsEmpty()
    {
        var request = new DeleteShoppingListRequest { ShoppingListId = Guid.Empty };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.ShoppingListId);
    }

    [Fact]
    public void Should_NotHaveError_When_ShoppingListIdIsValid()
    {
        var request = new DeleteShoppingListRequest { ShoppingListId = Guid.NewGuid() };
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
