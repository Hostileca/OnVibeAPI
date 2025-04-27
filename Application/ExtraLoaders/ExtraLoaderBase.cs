namespace Application.ExtraLoaders;

public abstract class ExtraLoaderBase<TDto> : IExtraLoader<TDto>
{
    public abstract Task LoadExtraInformationAsync(TDto dto, CancellationToken cancellationToken);

    public async Task LoadExtraInformationAsync(IList<TDto> dtos, CancellationToken cancellationToken)
    {
        foreach (var dto in dtos)
        {
            await LoadExtraInformationAsync(dto, cancellationToken);
        }
    }
}