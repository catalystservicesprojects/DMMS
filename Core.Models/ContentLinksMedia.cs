using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMMS.Models
{
    public class ContentLinksMedia : BaseIdentity.BaseUser
    {
        public required Content Content { get; set; }
        public required int ContentId { get; set; }
        public required int MediaTypeId { get; set; }
        public required string Link { get; set; }
    }

    public enum MediaType 
    {
        Image = 0,
        Video = 1,
        View360 = 2,
        VReality = 3,
    }
}
