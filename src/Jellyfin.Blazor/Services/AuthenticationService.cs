using Jellyfin.Sdk;
using Microsoft.AspNetCore.Components.Authorization;
using SystemException = Jellyfin.Sdk.SystemException;

namespace Jellyfin.Blazor.Services;

/// <inheritdoc />
public class AuthenticationService : IAuthenticationService
{
    private readonly IUserClient _userClient;
    private readonly ISystemClient _systemClient;

    private readonly IStateService _stateService;
    private readonly SdkClientSettings _sdkClientSettings;
    private readonly JellyfinAuthStateProvider _authenticationStateProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationService"/> class.
    /// </summary>
    /// <param name="userClient">Instance of the <see cref="IUserClient"/> interface.</param>
    /// <param name="systemClient">Instance of the <see cref="ISystemClient"/> interface.</param>
    /// <param name="stateService">Instance of the <see cref="IStateService"/> interface.</param>
    /// <param name="sdkClientSettings">Instance of the <see cref="SdkClientSettings"/>.</param>
    /// <param name="authenticationStateProvider">Instance of the <see cref="AuthenticationStateProvider"/>.</param>
    public AuthenticationService(
        IUserClient userClient,
        ISystemClient systemClient,
        IStateService stateService,
        SdkClientSettings sdkClientSettings,
        JellyfinAuthStateProvider authenticationStateProvider)
    {
        _userClient = userClient;
        _systemClient = systemClient;
        _stateService = stateService;
        _sdkClientSettings = sdkClientSettings;
        _authenticationStateProvider = authenticationStateProvider;
    }

    /// <inheritdoc />
    public async Task<(bool Status, string? ErrorMessage)> AuthenticateAsync(
        string host,
        string username,
        string? password)
    {
        // Set baseurl.
        _sdkClientSettings.BaseUrl = host;

        // Unset access token.
        _sdkClientSettings.AccessToken = null;

        try
        {
            _ = await _systemClient.GetPublicSystemInfoAsync()
                .ConfigureAwait(false);
        }
        catch (SystemException)
        {
            return (false, "Unable to connect to server");
        }

        try
        {
            var authResult = await _userClient.AuthenticateUserByNameAsync(new AuthenticateUserByName
                {
                    Username = username,
                    Pw = password
                })
                .ConfigureAwait(false);

            _stateService.SetAuthenticationResponse(host, authResult);
            _authenticationStateProvider.StateChanged();
            return (true, null);
        }
        catch (UserException)
        {
            return (false, "Invalid username or password");
        }
    }

    /// <inheritdoc />
    public void Logout()
    {
        _sdkClientSettings.BaseUrl = null;
        _sdkClientSettings.AccessToken = null;
        _stateService.ClearState();
        _authenticationStateProvider.StateChanged();
    }

    /// <inheritdoc />
    public async Task<bool> IsAuthenticatedAsync()
    {
        try
        {
            var user = await _userClient.GetCurrentUserAsync()
                .ConfigureAwait(false);
            _stateService.SetUser(user);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}