using FluentValidation;
using OnVibeAPI.Requests.Message;

namespace OnVibeAPI.Validators;

public class SendMessageRequestValidator : AbstractValidator<SendMessageRequest>
{
    public SendMessageRequestValidator()
    {
        RuleFor(x => x)
            .Must(x => !string.IsNullOrWhiteSpace(x.Text) || (x.Attachments != null && x.Attachments.Any()))
            .WithMessage("Either text or attachments must be provided")
            .OverridePropertyName("TextOrAttachments");

        RuleFor(x => x.Date)
            .GreaterThan(DateTime.UtcNow)
            .When(x => x.Date.HasValue)
            .WithMessage("Delay must be in the future");

        RuleFor(x => x.ChatId)
            .IsGuid();
    }
}