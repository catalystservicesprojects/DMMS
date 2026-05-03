using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DMMS.Models;
using DMMS.WebMVC.App.Models;
using Environment = DMMS.Models.Environment;

namespace DMMS.WebMVC.App.Data
{
    public class DMMSWebMVCAppContext : DbContext
    {
        public DMMSWebMVCAppContext (DbContextOptions<DMMSWebMVCAppContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<CMSType> CMSTypes { get; set; } = default!;
        public DbSet<Company> Companies { get; set; } = default!;


        public DbSet<Content> Content { get; set; } = default!;
        public DbSet<ContentMeta> ContentMetas { get; set; } = default!;
        public DbSet<ContentSEO> ContentSEO { get; set; } = default!;
        public DbSet<ContentSMO> ContentSMO { get; set; } = default!;
        public DbSet<ContentCoded> ContentCodeds { get; set; } = default!;
        public DbSet<ContentEdited> ContentEditeds { get; set; } = default!;
        public DbSet<ContentGenerated> ContentGenerated { get; set; } = default!;
        public DbSet<ContentLinksAffiliate> ContentLinksAffiliate { get; set; } = default!;
        public DbSet<ContentLinksExternalLink> ContentLinksExternalLinks { get; set; } = default!;
        public DbSet<ContentLinksMedia> ContentLinksMedia { get; set; } = default!;
        public DbSet<ContentLinksReference> ContentLinksReference { get; set; } = default!;
        public DbSet<ContentLinksSocial> ContentLinksSocial { get; set; } = default!;
        public DbSet<ContentRaw> ContentRaw { get; set; } = default!;
        public DbSet<ContentAi> ContentAi { get; set; } = default!;
        public DbSet<ContentResult> ContentResult { get; set; } = default!;
        public DbSet<ContentSection> ContentSections { get; set; } = default!;
        public DbSet<ContentSocial> ContentSocial { get; set; } = default!;
        public DbSet<ContentType> ContentTypes { get; set; } = default!;


        public DbSet<Environment> Environments { get; set; } = default!;
        public DbSet<InputTemplate> InputTemplates { get; set; } = default!;
        public DbSet<OnlineAccount> OnlineAccount { get; set; } = default!;


        public DbSet<KeywordStat> Keywords { get; set; } = default!;
        public DbSet<KeywordProvider> KeywordProviders { get; set; } = default!;
        public DbSet<KeywordMap> KeywordMap { get; set; } = default!;
        public DbSet<KeywordTopic> KeywordTopics { get; set; } = default!;


        public DbSet<Niche> Niches { get; set; } = default!;
        public DbSet<Publisher> Publisher { get; set; } = default!;


        public DbSet<SellerEntity> SellerEntity { get; set; } = default!;
        public DbSet<SocialMediaType> SocialMediaType { get; set; } = default!;


        public DbSet<Tag> Tag { get; set; } = default!;
        public DbSet<User> User{ get; set; } = default!;


        public DbSet<Website> Website { get; set; } = default!;
        public DbSet<WebsiteColor> WebsiteColor { get; set; } = default!;
        public DbSet<WebsiteConfig> WebsiteConfig { get; set; } = default!;
        public DbSet<WebsiteOnlineAccount> WebsiteOnlineAccount { get; set; } = default!;
        public DbSet<WebsiteType> WebsiteType { get; set; } = default!;
        public DbSet<WebsiteContentType> WebsiteContentTypes { get; set; } = default!;
        public DbSet<WebsiteContentTypeMap> WebsiteContentTypeMaps { get; set; } = default!;
        public DbSet<WebsitePrompt> WebsitePrompts { get; set; } = default!;
        public DbSet<WebsiteLayout> WebsiteLayouts { get; set; } = default!;

        public DbSet<DMMS.Models.SellerEntityMap> SellerEntityMap { get; set; } = default!;
    }
}