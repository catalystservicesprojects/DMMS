using DMMS.Models;
using DMMS.Models.Base;
using DMMS.Models.BaseIdentity;

namespace DMMS.WebMVC.App.Models
{
    public class Website : BaseUserNamable
    {
        public required DMMS.Models.Environment Environment { get; set; }
        public required int EnvironmentId { get; set; }

        public required WebsiteType WebsiteType { get; set; }
        public required int WebsiteTypeId { get; set; }

        public required string Url { get; set; }
        public required string LogoLink { get; set; }
    }
}