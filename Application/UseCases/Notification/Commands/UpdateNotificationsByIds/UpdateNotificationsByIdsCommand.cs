using Application.UseCases.Base;
using MediatR;

namespace Application.UseCases.Notification.Commands.UpdateNotificationsByIds;
public sealed class UpdateNotificationsByIdsCommand : RequestBase<Unit>
{
    public IList<Guid> Ids { get; init; }
    public bool IsRead { get; init; }
}

