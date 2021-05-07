using Blazorise;

namespace Jellyfin.Blazor.WebUI
{
    /// <summary>
    /// Code-behind for app.
    /// </summary>
    public partial class App
    {
        private readonly Theme _theme = new Theme
        {
            ColorOptions = new ThemeColorOptions
            {
                Primary = "#45B1E8",
                Secondary = "#A65529",
            }
        };
    }
}