using Application.Dtos.Chat;
using Application.Dtos.Page;
using Application.ExtraLoaders;
using Contracts.DataAccess.Interfaces;
using Contracts.DataAccess.Models;
using Contracts.DataAccess.Models.Include;
using Mapster;
using MediatR;

namespace Application.UseCases.Chat.Queries.GetUserChats;

public class GetUserChatsQueryHandler(
    IChatRepository chatRepository,
    IExtraLoader<ChatReadDto> chatExtraLoader)
    : IRequestHandler<GetUserChatsQuery, PagedResponse<ChatReadDto>>
{
    public async Task<PagedResponse<ChatReadDto>> Handle(GetUserChatsQuery request, CancellationToken cancellationToken)
    {
        var chats = await chatRepository.GetUserChatsAsync(
            request.InitiatorId, 
            new ChatIncludes { IncludeChatMembers = true },
            request.PageData.Adapt<PageInfo>(), 
            cancellationToken);

        var chatsReadDto = chats.Adapt<IList<ChatReadDto>>();

        await chatExtraLoader.LoadExtraInformationAsync(chatsReadDto, request.InitiatorId, cancellationToken);
        
        var response = new PagedResponse<ChatReadDto>(chatsReadDto, request.PageData.PageNumber, request.PageData.PageSize);
        
        return response;
    }
}