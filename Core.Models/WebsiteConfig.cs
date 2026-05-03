using DMMS.Models.BaseIdentity;
using DMMS.WebMVC.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMMS.Models
{
    public class WebsiteConfig : BaseUserNamable
    {
        public required Website Website { get; set; }
        public required int WebsiteId { get; set; }

        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Host { get; set; }
        public required string CharSet { get; set; }
        public required string Collate { get; set; }
        public required string ConnectionString { get; set; }
    }
}
