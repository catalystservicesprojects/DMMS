using DMMS.Models;
using DMMS.Models.Base;
using DMMS.Models.BaseIdentity;
using System.ComponentModel.DataAnnotations;
using static DMMS.Utility.Enumerations.AuthenticationEnum;

namespace DMMS.WebMVC.App.Models
{
    public class WebsiteOnlineAccount : BaseUserNonNamable
    {
        public required Website Website { get; set; }
        public required int WebsiteId { get; set; }

        public required OnlineAccount OnlineAccount { get; set; }
        public required int OnlineAccountId { get; set; }

        [MaxLength(150)]
        public string? Provider { get; set; }

        [Required]
        public AuthType AuthType { get; set; }

        // API Key
        [MaxLength(500)]
        public string? ApiKey { get; set; }

        // Basic Auth
        [MaxLength(200)]
        public string? Username { get; set; }

        [MaxLength(500)]
        public string? PasswordHash { get; set; }

        // OAuth2
        [MaxLength(300)]
        public string? ClientId { get; set; }

        [MaxLength(500)]
        public string? ClientSecret { get; set; }

        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? TokenExpiry { get; set; }

        // JWT
        [MaxLength(500)]
        public string? JwtSecret { get; set; }

        [MaxLength(200)]
        public string? Issuer { get; set; }

        [MaxLength(200)]
        public string? Audience { get; set; }

        // Endpoint
        [MaxLength(500)]
        public string? BaseUrl { get; set; }

        public required string Profile { get; set; }
        public required string Link { get; set; }
        public required string Password { get; set; }
        public required string Secret { get; set; }
        public required string BearerToken { get; set; }
        public required string SessionToken { get; set; }
    }
}