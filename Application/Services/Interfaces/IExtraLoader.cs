namespace Application.Services.Interfaces;

public interface IExtraLoader<TDto>
{
    Task LoadExtraInformationAsync(TDto dto, CancellationToken cancellationToken = default);
    Task LoadExtraInformationAsync(IEnumerable<TDto> dto, CancellationToken cancellationToken = default);
}