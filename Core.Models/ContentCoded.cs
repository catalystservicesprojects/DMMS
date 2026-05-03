using DMMS.Models.Base;
using DMMS.Models.BaseIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMMS.Models
{
    public class ContentCoded : BaseUser
    {
        public required Content Content { get; set; }
        public required int ContentId { get; set; }
        public required string Value { get; set; }
    }
}
