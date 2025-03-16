using Application.Dtos.Chat;
using Application.Dtos.Page;
using Contracts.DataAccess.Interfaces;
using Contracts.DataAccess.Models;
using Contracts.DataAccess.Models.Include;
using Mapster;
using MediatR;

namespace Application.UseCases.Chat.Queries.GetUserChats;

public class GetUserChatsQueryHandler(
    IChatRepository chatRepository)
    : IRequestHandler<GetUserChatsQuery, PageResponse<ChatReadDto>>
{
    public async Task<PageResponse<ChatReadDto>> Handle(GetUserChatsQuery request, CancellationToken cancellationToken)
    {
        var chats = await chatRepository.GetUserChatsAsync(
            request.UserId, 
            new ChatIncludes { IncludeChatMembers = true },
            request.PageData.Adapt<PageInfo>(), 
            cancellationToken);

        var response = new PageResponse<ChatReadDto>(chats.Adapt<IList<ChatReadDto>>(), request.PageData.PageNumber, request.PageData.PageSize);
        
        return response;
    }
}