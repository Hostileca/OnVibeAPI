using Application.Dtos.Message;
using Application.Dtos.Page;
using Application.Helpers.PermissionsHelpers;
using Application.Services.Interfaces;
using Contracts.DataAccess.Interfaces;
using Contracts.DataAccess.Models;
using Contracts.DataAccess.Models.Include;
using Domain.Exceptions;
using Mapster;
using MediatR;

namespace Application.UseCases.Message.Queries.GetChatMessages;

public class GetChatMessagesQueryHandler(
    IChatRepository chatRepository,
    IMessageRepository messageRepository,
    IExtraLoader<MessageReadDto> messageExtraLoader)
    : IRequestHandler<GetChatMessagesQuery, PagedResponse<MessageReadDto>>
{
    public async Task<PagedResponse<MessageReadDto>> Handle(GetChatMessagesQuery request, CancellationToken cancellationToken)
    {
        var chat = await chatRepository.GetChatByIdAsync(
            request.ChatId,
            new ChatIncludes
            {
                IncludeChatMembers = true
            },
            cancellationToken);
        
        if (chat is null)
        {
            throw new NotFoundException(typeof(Domain.Entities.Chat), request.ChatId.ToString());
        }

        if (!ChatPermissionsHelper.IsUserHasAccessToChat(chat, request.InitiatorId))
        {
            throw new ForbiddenException("You don't have access to this chat");
        }
        
        var messages = await messageRepository.GetAvailableToUserMessagesAsync(
            request.ChatId,
            request.InitiatorId,
            new MessageIncludes
            {
                IncludeReactions = true,
                IncludeSender = true
            },
            request.PageData.Adapt<PageInfo>(),
            cancellationToken);

        var messagesReadDtos = messages.Adapt<IList<MessageReadDto>>();
        await messageExtraLoader.LoadExtraInformationAsync(messagesReadDtos, cancellationToken);
        var result = new PagedResponse<MessageReadDto>(messagesReadDtos, request.PageData.PageNumber, request.PageData.PageSize);
        
        return result;
    }
}