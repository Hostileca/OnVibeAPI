namespace Application.ExtraLoaders;

public interface IExtraLoader<TDto>
{
    Task LoadExtraInformationAsync(TDto dto, CancellationToken cancellationToken = default);
    Task LoadExtraInformationAsync(IList<TDto> dto, CancellationToken cancellationToken = default);
}