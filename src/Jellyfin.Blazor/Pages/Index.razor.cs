using AsyncAwaitBestPractices;
using Jellyfin.Blazor.Services;
using Jellyfin.Sdk;
using Microsoft.AspNetCore.Components;

namespace Jellyfin.Blazor.Pages;

/// <summary>
///     The dashboard page.
/// </summary>
public partial class Index
{
    private readonly object _libraryLock = new ();
    private IReadOnlyList<BaseItemDto>? _continueWatching;
    private (BaseItemDto, IReadOnlyList<BaseItemDto>?)[]? _libraries;
    private IReadOnlyList<BaseItemDto>? _nextUp;

    [Inject]
    private ILibraryService LibraryService { get; set; } = null!;

    [Inject]
    private ILogger<Index> Logger { get; set; } = null!;

    /// <inheritdoc />
    protected override void OnInitialized()
    {
        InitializeDashboard();
        base.OnInitialized();
    }

    private void InitializeDashboard()
    {
        LibraryService.GetContinueWatching()
            .ContinueWith(
                task =>
                {
                    if (task.IsCompletedSuccessfully)
                    {
                        _continueWatching = task.Result;
                        StateHasChanged();
                    }
                }, TaskScheduler.Default)
            .SafeFireAndForget();
        LibraryService.GetLibraries()
            .ContinueWith(task =>
            {
                if (task.IsCompletedSuccessfully)
                {
                    InitializeLibraries(task.Result);
                }
            }, TaskScheduler.Default)
            .SafeFireAndForget();
    }

    private void InitializeLibraries(IReadOnlyList<BaseItemDto> libraries)
    {
        var libraryIds = new Guid[libraries.Count];
        lock (_libraryLock)
        {
            _libraries = new (BaseItemDto, IReadOnlyList<BaseItemDto>?)[libraries.Count];
            for (var i = 0; i < libraries.Count; i++)
            {
                libraryIds[i] = libraries[i].Id;
                _libraries[i] = (libraries[i], null);
            }
        }

        StateHasChanged();
        Parallel.ForEachAsync(libraries, async (library, cancellationToken) =>
            {
                var index = Array.IndexOf(libraryIds, library.Id);
                var items = await LibraryService.GetRecentlyAdded(library.Id, cancellationToken)
                    .ConfigureAwait(false);
                lock (_libraryLock)
                {
                    _libraries[index] = (library, items);
                    StateHasChanged();
                }
            })
            .SafeFireAndForget();

        LibraryService.GetNextUp(libraryIds)
            .ContinueWith(
                nextUp =>
                {
                    if (nextUp.IsCompleted)
                    {
                        _nextUp = nextUp.Result;
                        StateHasChanged();
                    }
                }, TaskScheduler.Default)
            .SafeFireAndForget();
    }
}
