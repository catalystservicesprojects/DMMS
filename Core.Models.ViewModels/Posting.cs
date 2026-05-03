using DMMS.WebMVC.App.Models;

namespace DMMS.Models.ViewModels
{
    public class Posting
    {
        public required Content Content { get; set; }
        public required Website? Website { get; set; }
        public required List<Website> Websites { get; set; }
        public required List<SellerEntity> SellerEntities { get; set; }
        //public required List<Affiliate> Affiliates { get; set; }
        public required List<string> Images { get; set; }
        public required List<string> Videos { get; set; }
        public required List<string> Socials { get; set; }
        public required List<string> Articles { get; set; }
        public required List<string> Posts { get; set; }
        public ContentMeta? ContentMeta { get; set; }
        public ContentRaw? ContentRaw { get; set; }
        public ContentAi? ContentAi { get; set; }
        public List<ContentLinksAffiliate>? ContentLinksAffiliates { get; set; }
        public List<ContentLinksMedia>? ContentLinksMedias { get; set; }
        public List<ContentLinksReference>? ContentLinksReferences { get; set; }
        public List<ContentLinksSocial>? ContentLinksSocials { get; set; }
        public List<ContentLinksExternalLink>? ContentLinksExternals { get; set; }
        public List<ContentLinksReference>? PreviousContentArticleLinksReferences { get; set; }
        public List<ContentLinksReference>? PreviousContentProductLinksReferences { get; set; }
        public string? ProductHtml { get; set; }
    }
}