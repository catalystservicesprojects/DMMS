using DMMS.Models.Base;
using DMMS.Models.BaseIdentity;
using DMMS.WebMVC.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMMS.Models
{
    public class Publisher : BaseUserNamable
    {
        public required Website Website { get; set; }
        public required int WebsiteId { get; set; }
        public required int PasswordHash { get; set; }
    }
}