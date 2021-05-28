namespace Jellyfin.Blazor.WebUI.Shared
{
    /// <summary>
    /// The main layout.
    /// </summary>
    public partial class MainLayout
    {
        /// <summary>
        /// Gets or sets a value indicating whether to display the sidebar.
        /// </summary>
        /// <remarks>
        /// This is data-bound to the <see cref="NavMenu"/> and <see cref="TopNavMenu"/>.
        /// </remarks>
        private bool ShowSidebar { get; set; }
    }
}