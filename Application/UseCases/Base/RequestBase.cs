using MediatR;

namespace Application.UseCases.Base;

public abstract class RequestBase<TReturn> : IRequest<TReturn>
{
    public Guid InitiatorId { get; init; }
}