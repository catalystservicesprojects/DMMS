using DMMS.Models.BaseIdentity;
using DMMS.WebMVC.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMMS.Models
{
    public class WebsiteContentType : BaseUserNamable
    {
        public required WebsiteType WebsiteType { get; set; }
        public required int WebsiteTypeId { get; set; }
    }
}
