using DMMS.Models;
using DMMS.Models.Base;
using DMMS.Models.BaseIdentity;

namespace DMMS.WebMVC.App.Models
{
    public class WebsitePrompt : BaseUserNamable
    {
        public required Website Website { get; set; }
        public required int WebsiteId { get; set; }

        public required int WebsiteContentTypeMapId { get; set; }
        public required string Prompt { get; set; }
    }
}