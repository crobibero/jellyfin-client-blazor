using System.Threading.Tasks;
using Jellyfin.Blazor.Services;
using Jellyfin.Blazor.WebUI.PageModels;
using Microsoft.AspNetCore.Components;

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

        [Inject]
        private NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        private IAuthenticationService AuthenticationService { get; set; } = null!;

        private async Task HandleLogin()
        {
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
            finally
            {
                _loading = false;
            }
        }
    }
}