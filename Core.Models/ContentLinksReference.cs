using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMMS.Models
{
    public class ContentLinksReference : BaseIdentity.BaseUser

    {
        public required Content Content { get; set; }
        public required int ContentId { get; set; }
        public required int ReferenceTypeId { get; set; }
        public string? Price { get; set; }
        public required string Title { get; set; }
        public required string ImageLink { get; set; }
        public required string Link { get; set; }
    }

    public enum ReferenceType
    {
        Article = 0,
        Post = 1
    }
}