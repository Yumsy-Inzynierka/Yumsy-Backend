using System.Data;
using FluentValidation;

namespace Yumsy_Backend.Features.Comments.AddComment;

public class AddCommentValidator : AbstractValidator<AddCommentRequest>
{
    public AddCommentValidator()
    {
        RuleFor(c => c.Content)
            .NotEmpty().WithMessage("Content cannot be empty")
            .MaximumLength(400).WithMessage("Content cannot be longer than 400 characters");

        RuleFor(c => c.PostId)
            .NotEmpty().WithMessage("Post ID cannot be empty");
        
        RuleFor(c => c.ParentCommentId)
            .Must(id => id == null || id != Guid.Empty)
            .WithMessage("ParentCommentId must be a valid GUID or null");
    }
}