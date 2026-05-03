using DMMS.Models.Base;
using DMMS.Models.BaseIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMMS.Models
{
    public class ContentSMO : BaseUserNamable
    {
        public required string ShortName { get; set; }
        public required string ShortDescription { get; set; }
        //public required int Version { get; set; }
    }
}