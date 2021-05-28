using Microsoft.AspNetCore.Components;

namespace Jellyfin.Blazor.WebUI.Shared
{
    /// <summary>
    /// The top nav menu.
    /// </summary>
    public partial class TopNavMenu
    {
        private bool _showSidebar;

        /// <summary>
        /// Gets or sets a value indicating whether the sidebar should be shown.
        /// </summary>
        [Parameter]
        public bool ShowSidebar
        {
            get => _showSidebar;
            set
            {
                if (_showSidebar == value)
                {
                    return;
                }

                _showSidebar = value;
                ShowSidebarChanged?.InvokeAsync(value);
            }
        }

        /// <summary>
        /// Gets or sets the event callback when the sidebar shown event changes.
        /// </summary>
        [Parameter]
        public EventCallback<bool>? ShowSidebarChanged { get; set; }

        private void ToggleMobileSidebar()
        {
            ShowSidebar = !ShowSidebar;
        }
    }
}