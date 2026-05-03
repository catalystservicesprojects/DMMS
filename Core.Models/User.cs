using DMMS.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMMS.Models
{
    public class User : BaseNamable
    {
        public required Company Company { get; set; }
        public int CompanyId { get; set; }
        public required string Email { get; set; }
        public required string Mobile { get; set; }
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }
    }
}
