using DMMS.Models;
using DMMS.Models.Base;
using DMMS.Models.BaseIdentity;

namespace DMMS.WebMVC.App.Models
{
    public class WebsiteColor : BaseUserNamable
    {
        public required Website Website { get; set; }
        public required int WebsiteId { get; set; }

        public required string BackgroundColor { get; set; }
        public required string TextColor { get; set; }
        public required string BorderColor { get; set; }
        public required string BackLinkColor { get; set; }
        public required string Style { get; set; }
        public required string Class { get; set; }
        public required int OpenIn { get; set; }
    }
}