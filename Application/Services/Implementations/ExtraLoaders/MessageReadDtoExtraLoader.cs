using Application.Dtos.Message;
using Contracts.DataAccess.Interfaces;

namespace Application.Services.Implementations.ExtraLoaders;

public class MessageReadDtoExtraLoader(IAttachmentRepository attachmentRepository) : ExtraLoaderBase<MessageReadDto>
{
    public override async Task LoadExtraInformationAsync(MessageReadDto dto, CancellationToken cancellationToken = default)
    {
        dto.AttachmentsIds = await attachmentRepository.GetAttachmentsIdsByMessageIdAsync(dto.Id, cancellationToken);
    }
}