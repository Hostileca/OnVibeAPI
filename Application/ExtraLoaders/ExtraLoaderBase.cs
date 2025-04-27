namespace Application.ExtraLoaders;

public abstract class ExtraLoaderBase<TDto> : IExtraLoader<TDto>
{
    public abstract Task LoadExtraInformationAsync(TDto dto, Guid initiatorId, CancellationToken cancellationToken);

    public async Task LoadExtraInformationAsync(IList<TDto> dtos, Guid initiatorId, CancellationToken cancellationToken)
    {
        foreach (var dto in dtos)
        {
            await LoadExtraInformationAsync(dto, initiatorId, cancellationToken);
        }
    }
}