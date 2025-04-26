namespace Application.Dtos.ExtraLoaders;

public abstract class ExtraLoaderBase<TDto> : IExtraLoader<TDto>
{
    public abstract Task LoadExtraInformationAsync(TDto dto, CancellationToken cancellationToken);

    public Task LoadExtraInformationAsync(IList<TDto> dtos, CancellationToken cancellationToken)
    {
        var tasks = dtos.Select(dto => LoadExtraInformationAsync(dto, cancellationToken)).ToList();
        return Task.WhenAll(tasks);
    }
}