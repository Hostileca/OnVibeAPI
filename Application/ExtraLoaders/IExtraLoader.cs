namespace Application.ExtraLoaders;

public interface IExtraLoader<TDto>
{
    Task LoadExtraInformationAsync(TDto dto, CancellationToken cancellationToken, Guid? initiatorId);
    Task LoadExtraInformationAsync(IList<TDto> dtos, CancellationToken cancellationToken, Guid? initiatorId);
}