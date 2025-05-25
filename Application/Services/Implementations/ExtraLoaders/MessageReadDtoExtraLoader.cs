using Application.Dtos.Message;
using Application.Services.Interfaces;
using Contracts.DataAccess.Interfaces;

namespace Application.Services.Implementations.ExtraLoaders;

public class MessageReadDtoExtraLoader(
    IAttachmentRepository attachmentRepository,
    INotificationRepository notificationRepository,
    IUserContext userContext) 
    : ExtraLoaderBase<MessageReadDto>
{
    public override async Task LoadExtraInformationAsync(MessageReadDto dto, CancellationToken cancellationToken = default)
    {
        dto.AttachmentsIds = await attachmentRepository.GetAttachmentsIdsByMessageIdAsync(dto.Id, cancellationToken);
        dto.IsRead = await notificationRepository.IsMessageReadByUserAsync(userContext.InitiatorId, dto.Id, cancellationToken);
    }
}