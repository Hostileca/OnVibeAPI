using MediatR;

namespace Application.UseCases.Notification.Commands.UpdateNotificationsByIds;
public sealed class UpdateNotificationsByIdsCommand : IRequest<Unit>
{
    public IList<Guid> Ids { get; init; }
    public bool IsRead { get; init; }
    public Guid InitiatorId { get; init; }
}

