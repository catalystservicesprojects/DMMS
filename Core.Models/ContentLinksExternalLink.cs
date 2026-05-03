using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMMS.Models
{
    public class ContentLinksExternalLink : BaseIdentity.BaseUser
    {
        public required Content Content { get; set; }
        public required int ContentId { get; set; }
        public required string WordOrSentense { get; set; }
        public required string Link { get; set; }
    }
}