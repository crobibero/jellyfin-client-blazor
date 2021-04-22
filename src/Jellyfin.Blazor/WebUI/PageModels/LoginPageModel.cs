using System.ComponentModel.DataAnnotations;

namespace Jellyfin.Blazor.WebUI.PageModels
{
    /// <summary>
    /// The login page model.
    /// </summary>
    public class LoginPageModel
    {
        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        [Required]
        [Url]
        public string Host { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        [Required]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string? Password { get; set; }
    }
}