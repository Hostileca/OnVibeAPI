using Application.Services.Interfaces;

namespace Application.Services.Implementations.ExtraLoaders;

public abstract class ExtraLoaderBase<TDto> : IExtraLoader<TDto>
{
    public abstract Task LoadExtraInformationAsync(TDto dto, CancellationToken cancellationToken = default);

    public async Task LoadExtraInformationAsync(IList<TDto> dtos, CancellationToken cancellationToken = default)
    {
        foreach (var dto in dtos)
        {
            await LoadExtraInformationAsync(dto, cancellationToken);
        }
    }
}