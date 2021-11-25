using Blazor.Extensions.Logging;
using Jellyfin.Blazor;
using Jellyfin.Blazor.Services;
using Jellyfin.Sdk;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddLogging(logger =>
{
    logger.AddBrowserConsole();
});

builder.Services.AddScoped(_ => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

builder.Services.AddServices();

var host = builder.Build();
var clientSettings = host.Services.GetRequiredService<SdkClientSettings>();
clientSettings.InitializeClientSettings(
    "Jellyfin Blazor",
    "0.0.1",
    "Desktop",
    Guid.NewGuid().ToString("N"));

await host.RunAsync().ConfigureAwait(false);
