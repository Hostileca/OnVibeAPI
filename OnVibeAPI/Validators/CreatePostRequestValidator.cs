using FluentValidation;
using OnVibeAPI.Requests.Message;
using OnVibeAPI.Requests.Post;

namespace OnVibeAPI.Validators;

public class CreatePostRequestValidator : AbstractValidator<CreatePostRequest>{
    public CreatePostRequestValidator()
    {
        RuleFor(x => x)
            .Must(x => !string.IsNullOrWhiteSpace(x.Content) || (x.Attachments != null && x.Attachments.Any()))
            .WithMessage("Either text or attachments must be provided")
            .OverridePropertyName("TextOrAttachments");
    }
}