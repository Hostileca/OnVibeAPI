using Application.Dtos.Chat;
using Application.Dtos.Message;
using Application.Services.Interfaces;
using Contracts.DataAccess.Interfaces;
using Contracts.DataAccess.Models;
using Contracts.DataAccess.Models.Include;
using Mapster;

namespace Application.Services.Implementations.ExtraLoaders;

public class ChatReadDtoExtraLoader(
    IMessageRepository messageRepository,
    IUserContext userContext) 
    : ExtraLoaderBase<ChatReadDto>
{
    public override async Task LoadExtraInformationAsync(ChatReadDto dto, CancellationToken cancellationToken = default)
    {
        var message = (await messageRepository.GetAvailableToUserMessagesAsync(
            dto.Id, 
            userContext.InitiatorId,
            new MessageIncludes
            {
                IncludeReactions = true,
                IncludeSender = true
            },
            new PageInfo(1, 1),
            cancellationToken)).FirstOrDefault();

        if (message is not null)
        {
            dto.Preview = message.Adapt<MessageReadDto>();
        }
    }
}