using Application.Dtos.Chat;
using Application.Dtos.Message;
using Contracts.DataAccess.Interfaces;
using Contracts.DataAccess.Models;
using Contracts.DataAccess.Models.Include;
using Mapster;

namespace Application.Services.Implementations.ExtraLoaders;

public class ChatReadDtoExtraLoader(IMessageRepository messageRepository) : ExtraLoaderBase<ChatReadDto>
{
    public override async Task LoadExtraInformationAsync(ChatReadDto dto, CancellationToken cancellationToken = default)
    {
        var message = (await messageRepository.GetMessagesByChatIdAsync(
            dto.Id, 
            new PageInfo(1, 1), 
            new MessageIncludes
            {
                IncludeReactions = true,
                IncludeSender = true
            }, cancellationToken)).FirstOrDefault();

        if (message is not null)
        {
            dto.Preview = message.Adapt<MessageReadDto>();
        }
    }
}