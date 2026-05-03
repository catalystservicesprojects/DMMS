using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMMS.Models
{
    public class ContentLinksSocial : BaseIdentity.BaseUser
    {
        public required Content Content { get; set; }
        public required int ContentId { get; set; }
        public required SocialMediaType SocialMediaType { get; set; }
        public required int SocialMediaTypeId { get; set; }
        public required string Link { get; set; }
    }
}