using DMMS.Models.BaseIdentity;
using DMMS.WebMVC.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMMS.Models
{
    public class ContentMeta : BaseUser
    {
        public required Content? Content { get; set; }
        public required int ContentId { get; set; }
        //public required Website? Website { get; set; }
        public required int WebsiteId { get; set; }

        public required string Title { get; set; }
        public string? Link { get; set; }
        public string? ImageLink { get; set; }
        public string? Slug { get; set; }
        public required string PrimaryKeyword { get; set; }
        public string? SecondaryKeywords { get; set; }
        public string? TertiaryKeywords { get; set; }
        public string? LongTailKeywords { get; set; }
        public required string ShortName { get; set; }
        public string? Hook { get; set; }
        public string? HookSlug { get; set; }
        public string? Description { get; set; }
        public required string Author { get; set; }
        public required string Robots { get; set; }
        public required string Language { get; set; }
        public required string Revisit { get; set; }
    }
}
