using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Jellyfin.Blazor.Services;
using Jellyfin.Sdk;
using Microsoft.AspNetCore.Components;

namespace Jellyfin.Blazor.Components;

/// <summary>
/// The primary card component.
/// </summary>
public partial class PrimaryCardComponent
{
    private string? _imageUrl;
    private string? _title;
    private string? _subTitle;

    /// <summary>
    /// Gets or sets the item.
    /// </summary>
    [Parameter]
    [Required]
    public BaseItemDto Item { get; set; } = null!;

    [Inject]
    private IStateService StateService { get; set; } = null!;

    /// <inheritdoc />
    protected override Task OnInitializedAsync()
    {
        var host = StateService.GetHost();
        // Get parent instead of self if it exists.
        var imageItemId = Item.SeriesId ?? Item.Id;

        _imageUrl = $"{host}/Items/{imageItemId}/Images/{ImageType.Primary}?quality=90&maxWidth=960";

        switch (Item.Type)
        {
            case BaseItemKind.Episode:
                _title = Item.SeriesName;
                _subTitle = $"S{Item.ParentIndexNumber} E{Item.IndexNumber} {Item.Name}";
                break;
            case BaseItemKind.Season:
                _title = Item.SeriesName;
                _subTitle = Item.SeasonName;
                break;
            default:
                _title = Item.Name;
                _subTitle = Item.ProductionYear?.ToString(CultureInfo.InvariantCulture);
                break;
        }

        StateHasChanged();
        return base.OnInitializedAsync();
    }
}
