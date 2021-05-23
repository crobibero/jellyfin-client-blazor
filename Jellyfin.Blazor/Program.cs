using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazor.Extensions.Logging;
using Jellyfin.Blazor.Services;
using Jellyfin.Sdk;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Jellyfin.Blazor
{
    /// <summary>
    /// The main program.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Main entrypoint.
        /// </summary>
        /// <param name="args">The program arguments.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddLogging(logger =>
            {
                logger.AddBrowserConsole();
            });

            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<JellyfinAuthStateProvider, JellyfinAuthStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(p => p.GetRequiredService<JellyfinAuthStateProvider>());

            // Add external services
            builder.Services.AddSingleton(_ => new HttpClient
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            });

            // Register 3rd party services

            // Register services
            builder.Services.AddSingleton<IStateService, StateService>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<ILibraryService, LibraryService>();

            // Register sdk services
            builder.Services.AddSingleton<SdkClientSettings>();

            builder.Services.AddHttpClient<IActivityLogClient, ActivityLogClient>();
            builder.Services.AddHttpClient<IApiKeyClient, ApiKeyClient>();
            builder.Services.AddHttpClient<IArtistsClient, ArtistsClient>();
            builder.Services.AddHttpClient<IAudioClient, AudioClient>();
            builder.Services.AddHttpClient<IBrandingClient, BrandingClient>();
            builder.Services.AddHttpClient<IChannelsClient, ChannelsClient>();
            builder.Services.AddHttpClient<ICollectionClient, CollectionClient>();
            builder.Services.AddHttpClient<IConfigurationClient, ConfigurationClient>();
            builder.Services.AddHttpClient<IDashboardClient, DashboardClient>();
            builder.Services.AddHttpClient<IDevicesClient, DevicesClient>();
            builder.Services.AddHttpClient<IDisplayPreferencesClient, DisplayPreferencesClient>();
            builder.Services.AddHttpClient<IDlnaClient, DlnaClient>();
            builder.Services.AddHttpClient<IDlnaServerClient, DlnaServerClient>();
            builder.Services.AddHttpClient<IDynamicHlsClient, DynamicHlsClient>();
            builder.Services.AddHttpClient<IEnvironmentClient, EnvironmentClient>();
            builder.Services.AddHttpClient<IFilterClient, FilterClient>();
            builder.Services.AddHttpClient<IGenresClient, GenresClient>();
            builder.Services.AddHttpClient<IHlsSegmentClient, HlsSegmentClient>();
            builder.Services.AddHttpClient<IImageClient, ImageClient>();
            builder.Services.AddHttpClient<IImageByNameClient, ImageByNameClient>();
            builder.Services.AddHttpClient<IInstantMixClient, InstantMixClient>();
            builder.Services.AddHttpClient<IItemLookupClient, ItemLookupClient>();
            builder.Services.AddHttpClient<IItemRefreshClient, ItemRefreshClient>();
            builder.Services.AddHttpClient<IItemsClient, ItemsClient>();
            builder.Services.AddHttpClient<ILibraryClient, LibraryClient>();
            builder.Services.AddHttpClient<IItemUpdateClient, ItemUpdateClient>();
            builder.Services.AddHttpClient<ILibraryStructureClient, LibraryStructureClient>();
            builder.Services.AddHttpClient<ILiveTvClient, LiveTvClient>();
            builder.Services.AddHttpClient<ILocalizationClient, LocalizationClient>();
            builder.Services.AddHttpClient<IMediaInfoClient, MediaInfoClient>();
            builder.Services.AddHttpClient<IMoviesClient, MoviesClient>();
            builder.Services.AddHttpClient<IMusicGenresClient, MusicGenresClient>();
            builder.Services.AddHttpClient<INotificationsClient, NotificationsClient>();
            builder.Services.AddHttpClient<IPackageClient, PackageClient>();
            builder.Services.AddHttpClient<IPersonsClient, PersonsClient>();
            builder.Services.AddHttpClient<IPlaylistsClient, PlaylistsClient>();
            builder.Services.AddHttpClient<IPlaystateClient, PlaystateClient>();
            builder.Services.AddHttpClient<IPluginsClient, PluginsClient>();
            builder.Services.AddHttpClient<IQuickConnectClient, QuickConnectClient>();
            builder.Services.AddHttpClient<IRemoteImageClient, RemoteImageClient>();
            builder.Services.AddHttpClient<IScheduledTasksClient, ScheduledTasksClient>();
            builder.Services.AddHttpClient<ISearchClient, SearchClient>();
            builder.Services.AddHttpClient<ISessionClient, SessionClient>();
            builder.Services.AddHttpClient<IStartupClient, StartupClient>();
            builder.Services.AddHttpClient<IStudiosClient, StudiosClient>();
            builder.Services.AddHttpClient<ISubtitleClient, SubtitleClient>();
            builder.Services.AddHttpClient<ISuggestionsClient, SuggestionsClient>();
            builder.Services.AddHttpClient<ISyncPlayClient, SyncPlayClient>();
            builder.Services.AddHttpClient<ISystemClient, SystemClient>();
            builder.Services.AddHttpClient<ITimeSyncClient, TimeSyncClient>();
            builder.Services.AddHttpClient<ITrailersClient, TrailersClient>();
            builder.Services.AddHttpClient<ITvShowsClient, TvShowsClient>();
            builder.Services.AddHttpClient<IUniversalAudioClient, UniversalAudioClient>();
            builder.Services.AddHttpClient<IUserClient, UserClient>();
            builder.Services.AddHttpClient<IUserLibraryClient, UserLibraryClient>();
            builder.Services.AddHttpClient<IUserViewsClient, UserViewsClient>();
            builder.Services.AddHttpClient<IVideoAttachmentsClient, VideoAttachmentsClient>();
            builder.Services.AddHttpClient<IVideoHlsClient, VideoHlsClient>();
            builder.Services.AddHttpClient<IVideosClient, VideosClient>();
            builder.Services.AddHttpClient<IYearsClient, YearsClient>();

            var host = builder.Build();
            var clientSettings = host.Services.GetRequiredService<SdkClientSettings>();
            clientSettings.InitializeClientSettings(
                "Jellyfin Blazor",
                "0.0.1",
                "Desktop",
                Guid.NewGuid().ToString("N"));
            await host.RunAsync().ConfigureAwait(false);
        }
    }
}