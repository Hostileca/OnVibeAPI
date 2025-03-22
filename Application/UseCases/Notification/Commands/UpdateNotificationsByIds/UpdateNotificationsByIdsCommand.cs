using MediatR;

namespace Application.UseCases.Notification.Commands.UpdateNotificationsByIds;

public sealed record UpdateNotificationsByIdsCommand(IList<Guid> Ids, Guid UserId, bool IsRead) : IRequest;