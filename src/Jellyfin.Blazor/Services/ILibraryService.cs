using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jellyfin.Sdk;

namespace Jellyfin.Blazor.Services
{
    /// <summary>
    /// The library service interface.
    /// </summary>
    public interface ILibraryService
    {
        /// <summary>
        /// Gets the library by id.
        /// </summary>
        /// <param name="id">The library id.</param>
        /// <returns>The library.</returns>
        Task<BaseItemDto?> GetLibrary(Guid id);

        /// <summary>
        /// Gets the library items.
        /// </summary>
        /// <param name="library">The library dto.</param>
        /// <returns>The library items.</returns>
        Task<BaseItemDtoQueryResult> GetLibraryItems(BaseItemDto library);

        /// <summary>
        /// Gets the next up items.
        /// </summary>
        /// <param name="libraryIds">The list of library ids.</param>
        /// <returns>The next up items.</returns>
        Task<IReadOnlyList<BaseItemDto>> GetNextUp(IReadOnlyList<Guid> libraryIds);

        /// <summary>
        /// Gets the continue watching items.
        /// </summary>
        /// <returns>The continue watching items.</returns>
        Task<IReadOnlyList<BaseItemDto>> GetContinueWatching();

        /// <summary>
        /// Gets the recently added items.
        /// </summary>
        /// <param name="libraryId">The library id.</param>
        /// <returns>The recently added library items.</returns>
        Task<IReadOnlyList<BaseItemDto>> GetRecentlyAdded(Guid libraryId);
    }
}