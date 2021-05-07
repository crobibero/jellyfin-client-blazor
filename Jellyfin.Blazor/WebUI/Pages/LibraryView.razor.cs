using System;
using System.Threading.Tasks;
using Jellyfin.Blazor.Services;
using Jellyfin.Sdk;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace Jellyfin.Blazor.WebUI.Pages
{
    /// <summary>
    /// The library view.
    /// </summary>
    public partial class LibraryView
    {
        private BaseItemDto? _library;

        /// <summary>
        /// Gets or sets the library id.
        /// </summary>
        [Parameter]
        public Guid LibraryId { get; set; }

        [Inject]
        private ILibraryService LibraryService { get; set; } = null!;

        /// <inheritdoc />
        protected override async Task OnParametersSetAsync()
        {
            _library = null;
            _library = await LibraryService.GetLibrary(LibraryId)
                .ConfigureAwait(false);
            await base.OnParametersSetAsync()
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Get the requested items.
        /// </summary>
        /// <param name="request">The item request.</param>
        /// <returns>The resulting items.</returns>
        protected async ValueTask<ItemsProviderResult<BaseItemDto>> LoadItemsAsync(ItemsProviderRequest request)
        {
            if (_library is null)
            {
                return default;
            }

            var queryResult = await LibraryService.GetLibraryItems(
                    _library,
                    request.Count,
                    request.StartIndex)
                .ConfigureAwait(false);
            return new ItemsProviderResult<BaseItemDto>(queryResult.Items, queryResult.TotalRecordCount);
        }
    }
}