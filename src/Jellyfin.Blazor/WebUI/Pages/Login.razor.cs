using System;
using System.Threading.Tasks;
using Blazorise;
using Jellyfin.Blazor.Services;
using Jellyfin.Blazor.WebUI.PageModels;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace Jellyfin.Blazor.WebUI.Pages
{
    /// <summary>
    /// The login page.
    /// </summary>
    public partial class Login
    {
        private readonly LoginPageModel _loginPageModel = new ();
        private bool _loading;
        private string? _error;
        private Validations? _validations;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        private IAuthenticationService AuthenticationService { get; set; } = null!;

        [Inject]
        private ILogger<Login> Logger { get; set; } = null!;

        private async Task HandleLogin()
        {
            if (_validations?.ValidateAll() != true)
            {
                return;
            }

            _loading = true;
            try
            {
                var (status, errorMessage) = await AuthenticationService.AuthenticateAsync(
                        _loginPageModel.Host,
                        _loginPageModel.Username,
                        _loginPageModel.Password)
                    .ConfigureAwait(false);
                if (status)
                {
                    NavigationManager.NavigateTo(string.Empty);
                }
                else
                {
                    _error = errorMessage;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unhandled exception");
                _error = "An unknown error occurred";
            }
            finally
            {
                _loading = false;
            }
        }
    }
}