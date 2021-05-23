using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HeroIcons.Blazor;
using HeroIcons.Blazor.Solid;
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
        private const string LinkCssClass = "hover:bg-indigo-600 group flex items-center px-2 py-2 text-sm font-medium rounded-md cursor-pointer";
        private const string IconCssClass = "mr-3 flex-shrink-0 h-6 w-6";

        private string currentRoute = string.Empty;
        private BaseItemDtoQueryResult? _views;

        [Inject]
        private IUserViewsClient UserViewsClient { get; set; } = null!;

        [Inject]
        private IStateService StateService { get; set; } = null!;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = null!;

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            _views = await UserViewsClient.GetUserViewsAsync(StateService.GetUserId())
                .ConfigureAwait(false);
            await base.OnInitializedAsync()
                .ConfigureAwait(false);
        }

        private void NavigateToDashboard()
        {
            currentRoute = string.Empty;
            NavigationManager.NavigateTo(currentRoute);
        }

        private void NavigateToView(Guid libraryId)
        {
            currentRoute = $"/view/{libraryId}";
            NavigationManager.NavigateTo(currentRoute);
        }

        private void NavigateToLogout()
        {
            NavigationManager.NavigateTo("/logout");
        }
    }
}