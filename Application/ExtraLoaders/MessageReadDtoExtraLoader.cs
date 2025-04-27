using Application.Dtos.Message;
using Contracts.DataAccess.Interfaces;

namespace Application.ExtraLoaders;

public class MessageReadDtoExtraLoader(IAttachmentRepository attachmentRepository) : ExtraLoaderBase<MessageReadDto>
{
    public override async Task LoadExtraInformationAsync(MessageReadDto dto, CancellationToken cancellationToken)
    {
        dto.AttachmentsIds = await attachmentRepository.GetAttachmentsIdsByMessageIdAsync(dto.Id, cancellationToken);
    }
}