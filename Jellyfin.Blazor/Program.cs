using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Threading.Tasks;
using Blazor.Extensions.Logging;
using Blazorise;
using Blazorise.Bulma;
using Blazorise.Icons.FontAwesome;
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
            builder.Services.AddLogging(builder =>
            {
                builder.AddBrowserConsole();
            });

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

            // Register 3rd party services
            builder.Services
                .AddBlazorise(options =>
                {
                    options.ChangeTextOnKeyPress = true;
                })
                .AddBulmaProviders()
                .AddFontAwesomeIcons();

            // Register services
            builder.Services.AddSingleton<IStateService, StateService>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<ILibraryService, LibraryService>();

            // Register sdk services
            builder.Services.AddSingleton<SdkClientSettings>();

            // builder.Services.AddSingleton<IActivityLogClient, ActivityLogClient>();
            // builder.Services.AddSingleton<IApiKeyClient, ApiKeyClient>();
            // builder.Services.AddSingleton<IArtistsClient, ArtistsClient>();
            // builder.Services.AddSingleton<IAudioClient, AudioClient>();
            // builder.Services.AddSingleton<IBrandingClient, BrandingClient>();
            // builder.Services.AddSingleton<IChannelsClient, ChannelsClient>();
            // builder.Services.AddSingleton<ICollectionClient, CollectionClient>();
            // builder.Services.AddSingleton<IConfigurationClient, ConfigurationClient>();
            // builder.Services.AddSingleton<IDashboardClient, DashboardClient>();
            // builder.Services.AddSingleton<IDevicesClient, DevicesClient>();
            // builder.Services.AddSingleton<IDisplayPreferencesClient, DisplayPreferencesClient>();
            // builder.Services.AddSingleton<IDlnaClient, DlnaClient>();
            // builder.Services.AddSingleton<IDlnaServerClient, DlnaServerClient>();
            // builder.Services.AddSingleton<IDynamicHlsClient, DynamicHlsClient>();
            // builder.Services.AddSingleton<IEnvironmentClient, EnvironmentClient>();
            // builder.Services.AddSingleton<IFilterClient, FilterClient>();
            // builder.Services.AddSingleton<IGenresClient, GenresClient>();
            // builder.Services.AddSingleton<IHlsSegmentClient, HlsSegmentClient>();
            builder.Services.AddSingleton<IImageClient, ImageClient>();
            // builder.Services.AddSingleton<IImageByNameClient, ImageByNameClient>();
            // builder.Services.AddSingleton<IInstantMixClient, InstantMixClient>();
            // builder.Services.AddSingleton<IItemLookupClient, ItemLookupClient>();
            // builder.Services.AddSingleton<IItemRefreshClient, ItemRefreshClient>();
            builder.Services.AddSingleton<IItemsClient, ItemsClient>();
            // builder.Services.AddSingleton<ILibraryClient, LibraryClient>();
            // builder.Services.AddSingleton<IItemUpdateClient, ItemUpdateClient>();
            // builder.Services.AddSingleton<ILibraryStructureClient, LibraryStructureClient>();
            // builder.Services.AddSingleton<ILiveTvClient, LiveTvClient>();
            // builder.Services.AddSingleton<ILocalizationClient, LocalizationClient>();
            // builder.Services.AddSingleton<IMediaInfoClient, MediaInfoClient>();
            // builder.Services.AddSingleton<IMoviesClient, MoviesClient>();
            // builder.Services.AddSingleton<IMusicGenresClient, MusicGenresClient>();
            // builder.Services.AddSingleton<INotificationsClient, NotificationsClient>();
            // builder.Services.AddSingleton<IPackageClient, PackageClient>();
            // builder.Services.AddSingleton<IPersonsClient, PersonsClient>();
            // builder.Services.AddSingleton<IPlaylistsClient, PlaylistsClient>();
            // builder.Services.AddSingleton<IPlaystateClient, PlaystateClient>();
            // builder.Services.AddSingleton<IPluginsClient, PluginsClient>();
            // builder.Services.AddSingleton<IQuickConnectClient, QuickConnectClient>();
            // builder.Services.AddSingleton<IRemoteImageClient, RemoteImageClient>();
            // builder.Services.AddSingleton<IScheduledTasksClient, ScheduledTasksClient>();
            // builder.Services.AddSingleton<ISearchClient, SearchClient>();
            // builder.Services.AddSingleton<ISessionClient, SessionClient>();
            // builder.Services.AddSingleton<IStartupClient, StartupClient>();
            // builder.Services.AddSingleton<IStudiosClient, StudiosClient>();
            // builder.Services.AddSingleton<ISubtitleClient, SubtitleClient>();
            // builder.Services.AddSingleton<ISuggestionsClient, SuggestionsClient>();
            // builder.Services.AddSingleton<ISyncPlayClient, SyncPlayClient>();
            builder.Services.AddSingleton<ISystemClient, SystemClient>();
            // builder.Services.AddSingleton<ITimeSyncClient, TimeSyncClient>();
            // builder.Services.AddSingleton<ITrailersClient, TrailersClient>();
            builder.Services.AddSingleton<ITvShowsClient, TvShowsClient>();
            // builder.Services.AddSingleton<IUniversalAudioClient, UniversalAudioClient>();
            builder.Services.AddSingleton<IUserClient, UserClient>();
            builder.Services.AddSingleton<IUserLibraryClient, UserLibraryClient>();
            builder.Services.AddSingleton<IUserViewsClient, UserViewsClient>();
            // builder.Services.AddSingleton<IVideoAttachmentsClient, VideoAttachmentsClient>();
            // builder.Services.AddSingleton<IVideoHlsClient, VideoHlsClient>();
            // builder.Services.AddSingleton<IVideosClient, VideosClient>();
            // builder.Services.AddSingleton<IYearsClient, YearsClient>();

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