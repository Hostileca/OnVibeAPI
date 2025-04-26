namespace Application.Dtos.ExtraLoaders;

public interface IExtraLoader<TDto>
{
    Task LoadExtraInformationAsync(TDto dto, CancellationToken cancellationToken);
    Task LoadExtraInformationAsync(IList<TDto> dtos, CancellationToken cancellationToken);
}