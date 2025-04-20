using Application.Dtos.Message;
using Application.Dtos.Page;
using Application.Helpers.PermissionsHelpers;
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
    IAttachmentRepository attachmentRepository)
    : IRequestHandler<GetChatMessagesQuery, PageResponse<MessageReadDto>>
{
    public async Task<PageResponse<MessageReadDto>> Handle(GetChatMessagesQuery request, CancellationToken cancellationToken)
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
        
        var messages = await messageRepository.GetMessagesByChatIdAsync(
            request.ChatId,
            request.PageData.Adapt<PageInfo>(),
            new MessageIncludes
            {
                IncludeReactions = true,
                IncludeSender = true
            },
            cancellationToken);

        var result = new PageResponse<MessageReadDto>(messages.Adapt<IList<MessageReadDto>>(), request.PageData.PageNumber, request.PageData.PageSize);
        
        await LoadExtraInfoAsync(result, cancellationToken);
        
        return result;
    }
    
    private async Task LoadExtraInfoAsync(PageResponse<MessageReadDto> response, CancellationToken cancellationToken)
    {
        foreach (var messageReadDto in response.Items)
        {
            messageReadDto.AttachmentsIds = await attachmentRepository.GetAttachmentsIdsByMessageIdAsync(messageReadDto.Id, cancellationToken);
        }
    }
}