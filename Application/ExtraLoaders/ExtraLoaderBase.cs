namespace Application.ExtraLoaders;

public abstract class ExtraLoaderBase<TDto> : IExtraLoader<TDto>
{
    public abstract Task LoadExtraInformationAsync(TDto dto, CancellationToken cancellationToken, Guid? initiatorId);

    public async Task LoadExtraInformationAsync(IList<TDto> dtos, CancellationToken cancellationToken, Guid? initiatorId)
    {
        foreach (var dto in dtos)
        {
            await LoadExtraInformationAsync(dto, cancellationToken, initiatorId);
        }
    }
}