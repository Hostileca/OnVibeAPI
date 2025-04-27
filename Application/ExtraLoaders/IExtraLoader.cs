namespace Application.ExtraLoaders;

public interface IExtraLoader<TDto>
{
    Task LoadExtraInformationAsync(TDto dto, Guid initiatorId, CancellationToken cancellationToken);
    Task LoadExtraInformationAsync(IList<TDto> dtos, Guid initiatorId, CancellationToken cancellationToken);
}