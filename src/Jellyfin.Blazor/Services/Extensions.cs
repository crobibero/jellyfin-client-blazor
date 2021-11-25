using Jellyfin.Sdk;
using Microsoft.AspNetCore.Components.Authorization;

namespace Jellyfin.Blazor.Services;

/// <summary>
/// Service Extensions.
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Adds the required services.
    /// </summary>
    /// <param name="serviceCollection">Instance of the <see cref="IServiceCollection"/>.</param>
    public static void AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddOptions();
        serviceCollection.AddAuthorizationCore();
        serviceCollection.AddScoped<JellyfinAuthStateProvider, JellyfinAuthStateProvider>();
        serviceCollection.AddScoped<AuthenticationStateProvider>(p => p.GetRequiredService<JellyfinAuthStateProvider>());
        
        // Register services
        serviceCollection.AddSingleton<IStateService, StateService>();
        serviceCollection.AddScoped<IAuthenticationService, AuthenticationService>();
        serviceCollection.AddScoped<ILibraryService, LibraryService>();

        // Register sdk services
        serviceCollection.AddSingleton<SdkClientSettings>();

        // TODO remove unused serviceCollection.
        serviceCollection.AddHttpClient<IActivityLogClient, ActivityLogClient>();
        serviceCollection.AddHttpClient<IApiKeyClient, ApiKeyClient>();
        serviceCollection.AddHttpClient<IArtistsClient, ArtistsClient>();
        serviceCollection.AddHttpClient<IAudioClient, AudioClient>();
        serviceCollection.AddHttpClient<IBrandingClient, BrandingClient>();
        serviceCollection.AddHttpClient<IChannelsClient, ChannelsClient>();
        serviceCollection.AddHttpClient<ICollectionClient, CollectionClient>();
        serviceCollection.AddHttpClient<IConfigurationClient, ConfigurationClient>();
        serviceCollection.AddHttpClient<IDashboardClient, DashboardClient>();
        serviceCollection.AddHttpClient<IDevicesClient, DevicesClient>();
        serviceCollection.AddHttpClient<IDisplayPreferencesClient, DisplayPreferencesClient>();
        serviceCollection.AddHttpClient<IDlnaClient, DlnaClient>();
        serviceCollection.AddHttpClient<IDlnaServerClient, DlnaServerClient>();
        serviceCollection.AddHttpClient<IDynamicHlsClient, DynamicHlsClient>();
        serviceCollection.AddHttpClient<IEnvironmentClient, EnvironmentClient>();
        serviceCollection.AddHttpClient<IFilterClient, FilterClient>();
        serviceCollection.AddHttpClient<IGenresClient, GenresClient>();
        serviceCollection.AddHttpClient<IHlsSegmentClient, HlsSegmentClient>();
        serviceCollection.AddHttpClient<IImageClient, ImageClient>();
        serviceCollection.AddHttpClient<IImageByNameClient, ImageByNameClient>();
        serviceCollection.AddHttpClient<IInstantMixClient, InstantMixClient>();
        serviceCollection.AddHttpClient<IItemLookupClient, ItemLookupClient>();
        serviceCollection.AddHttpClient<IItemRefreshClient, ItemRefreshClient>();
        serviceCollection.AddHttpClient<IItemsClient, ItemsClient>();
        serviceCollection.AddHttpClient<ILibraryClient, LibraryClient>();
        serviceCollection.AddHttpClient<IItemUpdateClient, ItemUpdateClient>();
        serviceCollection.AddHttpClient<ILibraryStructureClient, LibraryStructureClient>();
        serviceCollection.AddHttpClient<ILiveTvClient, LiveTvClient>();
        serviceCollection.AddHttpClient<ILocalizationClient, LocalizationClient>();
        serviceCollection.AddHttpClient<IMediaInfoClient, MediaInfoClient>();
        serviceCollection.AddHttpClient<IMoviesClient, MoviesClient>();
        serviceCollection.AddHttpClient<IMusicGenresClient, MusicGenresClient>();
        serviceCollection.AddHttpClient<INotificationsClient, NotificationsClient>();
        serviceCollection.AddHttpClient<IPackageClient, PackageClient>();
        serviceCollection.AddHttpClient<IPersonsClient, PersonsClient>();
        serviceCollection.AddHttpClient<IPlaylistsClient, PlaylistsClient>();
        serviceCollection.AddHttpClient<IPlaystateClient, PlaystateClient>();
        serviceCollection.AddHttpClient<IPluginsClient, PluginsClient>();
        serviceCollection.AddHttpClient<IQuickConnectClient, QuickConnectClient>();
        serviceCollection.AddHttpClient<IRemoteImageClient, RemoteImageClient>();
        serviceCollection.AddHttpClient<IScheduledTasksClient, ScheduledTasksClient>();
        serviceCollection.AddHttpClient<ISearchClient, SearchClient>();
        serviceCollection.AddHttpClient<ISessionClient, SessionClient>();
        serviceCollection.AddHttpClient<IStartupClient, StartupClient>();
        serviceCollection.AddHttpClient<IStudiosClient, StudiosClient>();
        serviceCollection.AddHttpClient<ISubtitleClient, SubtitleClient>();
        serviceCollection.AddHttpClient<ISuggestionsClient, SuggestionsClient>();
        serviceCollection.AddHttpClient<ISyncPlayClient, SyncPlayClient>();
        serviceCollection.AddHttpClient<ISystemClient, SystemClient>();
        serviceCollection.AddHttpClient<ITimeSyncClient, TimeSyncClient>();
        serviceCollection.AddHttpClient<ITrailersClient, TrailersClient>();
        serviceCollection.AddHttpClient<ITvShowsClient, TvShowsClient>();
        serviceCollection.AddHttpClient<IUniversalAudioClient, UniversalAudioClient>();
        serviceCollection.AddHttpClient<IUserClient, UserClient>();
        serviceCollection.AddHttpClient<IUserLibraryClient, UserLibraryClient>();
        serviceCollection.AddHttpClient<IUserViewsClient, UserViewsClient>();
        serviceCollection.AddHttpClient<IVideoAttachmentsClient, VideoAttachmentsClient>();
        serviceCollection.AddHttpClient<IVideoHlsClient, VideoHlsClient>();
        serviceCollection.AddHttpClient<IVideosClient, VideosClient>();
        serviceCollection.AddHttpClient<IYearsClient, YearsClient>();
    }
}
