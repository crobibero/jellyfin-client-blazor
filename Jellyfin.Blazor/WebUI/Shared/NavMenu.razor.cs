using System.Collections.Generic;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.Sidebar;
using Jellyfin.Blazor.Services;
using Jellyfin.Sdk;
using Microsoft.AspNetCore.Components;

namespace Jellyfin.Blazor.WebUI.Shared
{
    /// <summary>
    /// The nav menu.
    /// </summary>
    public partial class NavMenu
    {
        private BaseItemDtoQueryResult? _views;

        [Inject]
        private IUserViewsClient UserViewsClient { get; set; } = null!;

        [Inject]
        private IStateService StateService { get; set; } = null!;

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            _views = await UserViewsClient.GetUserViewsAsync(StateService.GetUserId())
                .ConfigureAwait(false);
            await base.OnInitializedAsync()
                .ConfigureAwait(false);
        }
    }
}