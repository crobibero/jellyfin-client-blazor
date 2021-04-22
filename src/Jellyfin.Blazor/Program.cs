using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Threading.Tasks;
using Jellyfin.Blazor.HttpClientHelpers;
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

            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<JellyfinAuthStateProvider, JellyfinAuthStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(p => p.GetRequiredService<JellyfinAuthStateProvider>());

            // Add external services

            // Register app-specific services
            /*builder.Services.AddHttpClient(NamedClient.Default, c =>
            {
                // TODO toggle between WASM and native HttpClient.
                // TODO replace with client name and version.
                c.DefaultRequestHeaders.UserAgent.Add(
                    new ProductInfoHeaderValue(
                        "Jellyfin-Blazor",
                        "0.0.1"));
                c.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json, 1.0));
                c.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("#1#*", 0.8));
            });*/
            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            });

            // Register services
            builder.Services.AddSingleton<IStateService, StateService>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<ILibraryService, LibraryService>();

            // Register sdk services
            builder.Services.AddSingleton<SdkClientSettings>();

            // builder.Services.AddScoped<IActivityLogClient, ActivityLogClient>();
            // builder.Services.AddScoped<IApiKeyClient, ApiKeyClient>();
            // builder.Services.AddScoped<IArtistsClient, ArtistsClient>();
            // builder.Services.AddScoped<IAudioClient, AudioClient>();
            // builder.Services.AddScoped<IBrandingClient, BrandingClient>();
            // builder.Services.AddScoped<IChannelsClient, ChannelsClient>();
            // builder.Services.AddScoped<ICollectionClient, CollectionClient>();
            // builder.Services.AddScoped<IConfigurationClient, ConfigurationClient>();
            // builder.Services.AddScoped<IDashboardClient, DashboardClient>();
            // builder.Services.AddScoped<IDevicesClient, DevicesClient>();
            // builder.Services.AddScoped<IDisplayPreferencesClient, DisplayPreferencesClient>();
            // builder.Services.AddScoped<IDlnaClient, DlnaClient>();
            // builder.Services.AddScoped<IDlnaServerClient, DlnaServerClient>();
            // builder.Services.AddScoped<IDynamicHlsClient, DynamicHlsClient>();
            // builder.Services.AddScoped<IEnvironmentClient, EnvironmentClient>();
            // builder.Services.AddScoped<IFilterClient, FilterClient>();
            // builder.Services.AddScoped<IGenresClient, GenresClient>();
            // builder.Services.AddScoped<IHlsSegmentClient, HlsSegmentClient>();
            builder.Services.AddScoped<IImageClient, ImageClient>();
            // builder.Services.AddScoped<IImageByNameClient, ImageByNameClient>();
            // builder.Services.AddScoped<IInstantMixClient, InstantMixClient>();
            // builder.Services.AddScoped<IItemLookupClient, ItemLookupClient>();
            // builder.Services.AddScoped<IItemRefreshClient, ItemRefreshClient>();
            builder.Services.AddScoped<IItemsClient, ItemsClient>();
            // builder.Services.AddScoped<ILibraryClient, LibraryClient>();
            // builder.Services.AddScoped<IItemUpdateClient, ItemUpdateClient>();
            // builder.Services.AddScoped<ILibraryStructureClient, LibraryStructureClient>();
            // builder.Services.AddScoped<ILiveTvClient, LiveTvClient>();
            // builder.Services.AddScoped<ILocalizationClient, LocalizationClient>();
            // builder.Services.AddScoped<IMediaInfoClient, MediaInfoClient>();
            // builder.Services.AddScoped<IMoviesClient, MoviesClient>();
            // builder.Services.AddScoped<IMusicGenresClient, MusicGenresClient>();
            // builder.Services.AddScoped<INotificationsClient, NotificationsClient>();
            // builder.Services.AddScoped<IPackageClient, PackageClient>();
            // builder.Services.AddScoped<IPersonsClient, PersonsClient>();
            // builder.Services.AddScoped<IPlaylistsClient, PlaylistsClient>();
            // builder.Services.AddScoped<IPlaystateClient, PlaystateClient>();
            // builder.Services.AddScoped<IPluginsClient, PluginsClient>();
            // builder.Services.AddScoped<IQuickConnectClient, QuickConnectClient>();
            // builder.Services.AddScoped<IRemoteImageClient, RemoteImageClient>();
            // builder.Services.AddScoped<IScheduledTasksClient, ScheduledTasksClient>();
            // builder.Services.AddScoped<ISearchClient, SearchClient>();
            // builder.Services.AddScoped<ISessionClient, SessionClient>();
            // builder.Services.AddScoped<IStartupClient, StartupClient>();
            // builder.Services.AddScoped<IStudiosClient, StudiosClient>();
            // builder.Services.AddScoped<ISubtitleClient, SubtitleClient>();
            // builder.Services.AddScoped<ISuggestionsClient, SuggestionsClient>();
            // builder.Services.AddScoped<ISyncPlayClient, SyncPlayClient>();
            builder.Services.AddScoped<ISystemClient, SystemClient>();
            // builder.Services.AddScoped<ITimeSyncClient, TimeSyncClient>();
            // builder.Services.AddScoped<ITrailersClient, TrailersClient>();
            builder.Services.AddScoped<ITvShowsClient, TvShowsClient>();
            // builder.Services.AddScoped<IUniversalAudioClient, UniversalAudioClient>();
            builder.Services.AddScoped<IUserClient, UserClient>();
            builder.Services.AddScoped<IUserLibraryClient, UserLibraryClient>();
            builder.Services.AddScoped<IUserViewsClient, UserViewsClient>();
            // builder.Services.AddScoped<IVideoAttachmentsClient, VideoAttachmentsClient>();
            // builder.Services.AddScoped<IVideoHlsClient, VideoHlsClient>();
            // builder.Services.AddScoped<IVideosClient, VideosClient>();
            // builder.Services.AddScoped<IYearsClient, YearsClient>();

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