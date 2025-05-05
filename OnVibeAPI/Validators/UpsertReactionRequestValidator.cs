using FluentValidation;
using OnVibeAPI.Requests.Reaction;

namespace OnVibeAPI.Validators;

public class UpsertReactionRequestValidator : AbstractValidator<UpsertReactionRequest>
{
    public UpsertReactionRequestValidator()
    {
        RuleFor(x => x.MessageId)
            .IsGuid();

        RuleFor(x => x.Emoji)
            .IsEmoji()
            .When(x => x.Emoji != null)
            .WithMessage("Invalid emoji");
    }
}