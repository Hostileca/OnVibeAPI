using Application.Dtos.Chat;
using Application.Dtos.Message;
using Application.Dtos.Page;
using Contracts.DataAccess.Interfaces;
using Contracts.DataAccess.Models;
using Contracts.DataAccess.Models.Include;
using Mapster;
using MediatR;

namespace Application.UseCases.Chat.Queries.GetUserChats;

public class GetUserChatsQueryHandler(
    IChatRepository chatRepository,
    IMessageRepository messageRepository)
    : IRequestHandler<GetUserChatsQuery, PageResponse<ChatReadDto>>
{
    public async Task<PageResponse<ChatReadDto>> Handle(GetUserChatsQuery request, CancellationToken cancellationToken)
    {
        var chats = await chatRepository.GetUserChatsAsync(
            request.UserId, 
            new ChatIncludes { IncludeChatMembers = true },
            request.PageData.Adapt<PageInfo>(), 
            cancellationToken);

        var chatsReadDto = chats.Adapt<IList<ChatReadDto>>();

        await LoadChatsPreview(chatsReadDto, cancellationToken);
        
        var response = new PageResponse<ChatReadDto>(chatsReadDto, request.PageData.PageNumber, request.PageData.PageSize);
        
        return response;
    }

    private async Task LoadChatsPreview(IList<ChatReadDto> chatReadDtos, CancellationToken cancellationToken)
    {
        foreach (var chatReadDto in chatReadDtos)
        {
            var message = (await messageRepository.GetMessagesByChatIdAsync(chatReadDto.Id, new PageInfo(1, 1), cancellationToken)).FirstOrDefault();

            chatReadDto.Preview = message.Adapt<MessageReadDto>();
        }
    }
}