using DMMS.Models;
using DMMS.Models.Requests;
using DMMS.Models.Response;
using DMMS.Models.ViewModels;
using DMMS.Utility.Helpers;
using DMMS.WebMVC.App.Data;
using DMMS.WebMVC.App.Helper;
using DMMS.WebMVC.App.Models;
using DMMS.WebMVC.App.Repositories;
using eLearning.Models.Requests;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Net.Http.Headers;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DMMS.WebMVC.App.Controllers
{
    public class PostingController : Controller
    {
        private readonly DMMSWebMVCAppContext _context;

        private readonly IWebHostEnvironment _env;

        private readonly IDbContextFactory _dynamic;
        public PostingController(DMMSWebMVCAppContext context, IWebHostEnvironment env, IDbContextFactory dynamic)
        {
            _context = context;
            _dynamic = dynamic;
            _env = env;
        }

        public IActionResult PostBuilder()
        {
            int contentId = Convert.ToInt32(HttpContext.Request.Query["ContentId"]);
            int websiteId = Convert.ToInt32(HttpContext.Request.Query["WebsiteId"]);
            int userId = Convert.ToInt32(HttpContext.Request.Query["UserId"]);

            Content content = _context.Content.Where(x => x.Id == contentId).First();
            Website? website = _context.Website.Where(x => x.Id == content.WebsiteId).FirstOrDefault();

            List<CMSType> cmsTypes = _context.CMSTypes.ToList();
            List<Website> websites = _context.Website.ToList();
            List<SellerEntity> sellerEntities = _context.SellerEntity.OrderBy(x => x.Id).ToList();
            ContentMeta? contentMeta = _context.ContentMetas.Where(x => x.ContentId == contentId).FirstOrDefault();
            ContentRaw? contentRaw = _context.ContentRaw.Where(x => x.ContentId == contentId).FirstOrDefault();
            ContentAi? contentAi = _context.ContentAi.Where(x => x.ContentId == contentId).FirstOrDefault();
            List<ContentLinksAffiliate> contentLinksAffiliates = _context.ContentLinksAffiliate
                .Where(x => x.ContentId == contentId).OrderBy(x => x.SellerEntityId).ToList();

            List<ContentLinksMedia> contentLinksMedias = _context.ContentLinksMedia.Where(x => x.ContentId == contentId).ToList();
            List<ContentLinksReference> contentLinksReferences = _context.ContentLinksReference.Where(x => x.ContentId == contentId).ToList();
            List<ContentLinksSocial> contentLinksSocials = _context.ContentLinksSocial.Where(x => x.ContentId == contentId).ToList();
            List<ContentLinksExternalLink> contentLinksExternalLinks = _context.ContentLinksExternalLinks.Where(x => x.ContentId == contentId).ToList();

            Content? previousSeoContent = _context.Content
                                        .Where(x => x.Id < contentId
                                            && x.WebsiteId == websiteId
                                            && x.UserId == userId
                                            && x.Status == 1)
                                        .OrderByDescending(x => x.Id)
                                        .FirstOrDefault();

            List<ContentLinksReference>? previousSeoContentLinkReferenses = null;
            List<ContentLinksReference>? previousSeoArticleContentLinkReferenses = null;
            List<ContentLinksReference>? previousSeoProductContentLinkReferenses = null;
            if (previousSeoContent != null)
            {
                previousSeoContentLinkReferenses = _context.ContentLinksReference.Where(x => x.ContentId == previousSeoContent.Id).ToList();
                previousSeoArticleContentLinkReferenses = previousSeoContentLinkReferenses.Where(x => x.ReferenceTypeId == 0)
                    .OrderByDescending(x => x.CreationDate).Take(3).ToList();
                previousSeoProductContentLinkReferenses = previousSeoContentLinkReferenses.Where(x => x.ReferenceTypeId == 1)
                    .OrderByDescending(x => x.CreationDate).Take(3).ToList();
            }

            List<string> Images = new List<string>
            {
                "image",
                "image",
                "image",
                "image",
                "image",
                "image",
                "image"
            };

            List<string> Videos = new List<string>
            {
                "video",
                "video",
                "video",
                "video",
                "video",
                "video",
                "video"
            };

            List<string> Social = new List<string>
            {
                "Facebook",
                "Instagram",
                "Threads",
                "Youtube",
                "LinkedIn",
                "Pintrest",
                "X"
            };

            List<string> Articles = new List<string>
            {
                "Article",
                "Article",
                "Article",
                "Article",
                "Article",
                "Article",
                "Article"
            };

            List<string> Posts = new List<string>
            {
                "Post",
                "Post",
                "Post",
                "Post",
                "Post",
                "Post",
                "Post"
            };

            //string path = Path.Combine(_env.WebRootPath, "htm", "product-v5.html");
            //string path = Path.Combine(_env.WebRootPath, "htm", "blog-v1.html");
            //string path = Path.Combine(_env.WebRootPath, "htm", "blog-simple.html");
            //string path = Path.Combine(_env.WebRootPath, "htm", "post_layout_classic.html");
            string path = "";
            if (HttpContext.Session.GetString("CompanyIdentity") == "8d81559afc6549a38635b9e983722638")
            {
                path = Path.Combine(_env.WebRootPath, "htm", "post_layout_classic_wiseweb.html");
            }
            else if (HttpContext.Session.GetString("CompanyIdentity") == "da3ffedb31914cea8070c900f7ae37bb")
            {
                path = Path.Combine(_env.WebRootPath, "htm", "post_layout_classic_others.html");
            }
            else if (HttpContext.Session.GetString("CompanyIdentity") == "50563356604b48d2b53521b5a7f59337")
            {
                path = Path.Combine(_env.WebRootPath, "htm", "post_layout_blog_koilakishiv.html");
            }
            else if (HttpContext.Session.GetString("CompanyIdentity") == "184a261ff4944f0a9470ddfff292939f")
            {
                //path = Path.Combine(_env.WebRootPath, "htm", "post_layout_classic_others.html");
                //path = Path.Combine(_env.WebRootPath, "htm", "post_layout_classic_uscpl.html");
                path = Path.Combine(_env.WebRootPath, "htm", "post_layout_classic_uscpl_blog.html");
            }
            string productHtml = System.IO.File.ReadAllText(path);

            Posting posting = new Posting()
            {
                Content = content,
                Website = website,
                Websites = websites,
                SellerEntities = sellerEntities,
                ContentRaw = contentRaw,
                ContentAi = contentAi,
                ContentLinksAffiliates = contentLinksAffiliates,
                ContentLinksMedias = contentLinksMedias,
                ContentLinksReferences = contentLinksReferences,
                PreviousContentArticleLinksReferences = previousSeoArticleContentLinkReferenses,
                PreviousContentProductLinksReferences = previousSeoProductContentLinkReferenses,
                ContentLinksSocials = contentLinksSocials,
                ContentLinksExternals = contentLinksExternalLinks,
                ContentMeta = contentMeta,
                Images = Images,
                Videos = Videos,
                Socials = Social,
                Articles = Articles,
                Posts = Posts,
                ProductHtml = productHtml
            };

            if (website != null)
            {
                ViewBag.Website = website.Url;
            }

            ViewBag.ContentId = contentId;
            ViewBag.Name = content.Name;
            //ViewBag.Slug = content.Name?.ToString().ToLower().Replace(' ', '-');
            //ViewBag.ShortName = content.ShortName;
            //ViewBag.PrimaryKeyword = content.Alias;
            //ViewBag.SecondaryKeyword = content.ShortDescription;

            ViewBag.Id = contentMeta.Id;
            ViewBag.ContentId = contentMeta.ContentId;
            ViewBag.Title = contentMeta.Title;
            ViewBag.Link = contentMeta.Link;
            ViewBag.ImageLink = contentMeta.ImageLink;
            ViewBag.Slug = contentMeta.Slug;
            ViewBag.ShortName = contentMeta.ShortName;
            ViewBag.WordpressSlug = SlugHelper.GenerateSlug(contentMeta.ShortName);
            ViewBag.PrimaryKeyword = contentMeta.PrimaryKeyword;
            ViewBag.SecondaryKeywords = contentMeta.SecondaryKeywords;
            ViewBag.TertiaryKeywords = contentMeta.TertiaryKeywords;
            ViewBag.WebsiteId = contentMeta.WebsiteId;
            ViewBag.Hook = contentMeta.Hook;
            ViewBag.HookSlug = contentMeta.HookSlug;
            ViewBag.Description = contentMeta.Description;
            ViewBag.Author = contentMeta.Author;
            ViewBag.Robots = contentMeta.Robots;
            ViewBag.Language = contentMeta.Language;
            ViewBag.Revisit = contentMeta.Revisit;
            return View(posting);
        }

        [HttpPost]
        public IActionResult GenerateSlug(string title)
        {
            var slug = SlugHelper.GenerateSlug(title);
            return Json(slug);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveRawContent(ContentRaw contentRaw)
        {
            try
            {
                if (contentRaw == null)
                    return BadRequest(new { status = "error", message = "No data received" });

                bool isNew = false;

                // Check if record exists (based on PK Id)
                var existing = await _context.ContentRaw
                                             .FirstOrDefaultAsync(x => x.ContentId == contentRaw.ContentId);

                if (existing == null)
                {
                    // INSERT
                    _context.ContentRaw.Add(contentRaw);
                    isNew = true;
                }
                else
                {
                    // UPDATE – overwrite properties
                    existing.ContentId = contentRaw.ContentId;
                    //existing.ContentRaw = contentRaw.ContentRaw;
                    existing.Value = contentRaw.Value;
                    existing.Guid = contentRaw.Guid;
                    existing.Identity = contentRaw.Identity;
                    existing.Status = contentRaw.Status;
                    existing.Tenant = contentRaw.Tenant;
                    existing.CreatedBy = contentRaw.CreatedBy;
                    existing.CreationDate = contentRaw.CreationDate;
                    existing.ModifiedBy = contentRaw.ModifiedBy;
                    existing.ModifiedDate = contentRaw.ModifiedDate;

                    // EF will automatically track "existing" and update it.
                }

                int affected = await _context.SaveChangesAsync();

                return Ok(new
                {
                    status = isNew ? "inserted" : "updated",
                    id = contentRaw.ContentId,
                    message = isNew ? affected + " Record inserted successfully" : affected + " Record updated successfully"
                });
            }
            catch (Exception ex)
            {
                // automatically triggers jQuery error/fail
                return StatusCode(500, new
                {
                    status = "error",
                    message = ex.Message
                });
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveAiContent(ContentAi contentAi)
        {
            try
            {
                if (contentAi == null)
                    return BadRequest(new { status = "error", message = "No data received" });

                bool isNew = false;

                // Check if record exists (based on PK Id)
                var existing = await _context.ContentAi
                                             .FirstOrDefaultAsync(x => x.ContentId == contentAi.ContentId);

                if (existing == null)
                {
                    // INSERT
                    _context.ContentAi.Add(contentAi);
                    isNew = true;
                }
                else
                {
                    // UPDATE – overwrite properties
                    existing.ContentId = contentAi.ContentId;
                    //existing.ContentRaw = contentAi.ContentRaw;
                    existing.Introduction = contentAi.Introduction;
                    existing.Value = contentAi.Value;
                    existing.Guid = contentAi.Guid;
                    existing.Identity = contentAi.Identity;
                    existing.Status = contentAi.Status;
                    existing.Tenant = contentAi.Tenant;
                    existing.CreatedBy = contentAi.CreatedBy;
                    existing.CreationDate = contentAi.CreationDate;
                    existing.ModifiedBy = contentAi.ModifiedBy;
                    existing.ModifiedDate = contentAi.ModifiedDate;

                    // EF will automatically track "existing" and update it.
                }

                int affected = await _context.SaveChangesAsync();

                return Ok(new
                {
                    status = isNew ? "inserted" : "updated",
                    id = contentAi.ContentId,
                    message = isNew ? affected + " Record inserted successfully" : affected + " Record updated successfully"
                });
            }
            catch (Exception ex)
            {
                // automatically triggers jQuery error/fail
                return StatusCode(500, new { status = "error", message = ex.Message });
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveAffiliateSingleData(ContentLinksAffiliate contentLinksAffiliate)
        {
            try
            {
                bool isNew = false;
                int affected = 0;

                if (contentLinksAffiliate == null)
                    return BadRequest(new { status = "error", message = "No data received" });

                if (string.IsNullOrEmpty(contentLinksAffiliate.Link) || string.IsNullOrEmpty(contentLinksAffiliate.Price))
                {
                    return Ok(new
                    {
                        id = contentLinksAffiliate.Id,
                        status = "skipped",
                        content_id = contentLinksAffiliate.ContentId,
                        status_code = 200,
                        message = "incomplete data received"
                    });
                }

                // Check if record exists (based on PK Id)
                var existing = await _context.ContentLinksAffiliate
                                             .FirstOrDefaultAsync(x => x.ContentId == contentLinksAffiliate.ContentId &&
                                             x.SellerEntityId == contentLinksAffiliate.SellerEntityId);

                if (existing == null)
                {
                    // INSERT
                    _context.ContentLinksAffiliate.Add(contentLinksAffiliate);
                    isNew = true;
                }
                else
                {
                    // UPDATE – overwrite properties
                    existing.ContentId = contentLinksAffiliate.ContentId;
                    existing.SellerEntityId = contentLinksAffiliate.SellerEntityId;
                    existing.SellerName = contentLinksAffiliate.SellerName;
                    existing.Price = contentLinksAffiliate.Price;
                    existing.Link = contentLinksAffiliate.Link;

                    existing.Guid = contentLinksAffiliate.Guid;
                    existing.Identity = contentLinksAffiliate.Identity;
                    existing.Status = contentLinksAffiliate.Status;
                    existing.Tenant = contentLinksAffiliate.Tenant;
                    existing.CreatedBy = contentLinksAffiliate.CreatedBy;
                    existing.CreationDate = contentLinksAffiliate.CreationDate;
                    existing.ModifiedBy = contentLinksAffiliate.ModifiedBy;
                    existing.ModifiedDate = contentLinksAffiliate.ModifiedDate;

                    // EF will automatically track "existing" and update it.
                }

                affected = await _context.SaveChangesAsync();

                return Ok(new
                {
                    id = contentLinksAffiliate.Id,
                    status = isNew ? "inserted" : "updated",
                    content_id = contentLinksAffiliate.ContentId,
                    status_code = 200,
                    message = isNew ? affected + " Record inserted successfully" : affected + " Record updated successfully"
                });
            }
            catch (Exception ex)
            {
                // automatically triggers jQuery error/fail
                return StatusCode(500, new { status = "error", message = ex.Message });
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveAffiliateData(List<ContentLinksAffiliate> contentLinksAffiliates)
        {
            try
            {
                List<Record> records = new List<Record>();

                if (contentLinksAffiliates == null)
                    return BadRequest(new { status = "error", message = "No data received" });

                foreach (ContentLinksAffiliate contentLinksAffiliate in contentLinksAffiliates)
                {
                    bool isNew = false;
                    // Check if record exists (based on PK Id)
                    var existing = await _context.ContentLinksAffiliate
                                                 .FirstOrDefaultAsync(x => x.ContentId == contentLinksAffiliate.ContentId &&
                                                 x.SellerEntityId == contentLinksAffiliate.SellerEntityId);

                    if (string.IsNullOrEmpty(contentLinksAffiliate.Link) || string.IsNullOrEmpty(contentLinksAffiliate.Price))
                    {
                        continue;
                    }
                    if (existing == null)
                    {
                        // INSERT
                        _context.ContentLinksAffiliate.Add(contentLinksAffiliate);
                        isNew = true;
                    }
                    else
                    {
                        // UPDATE – overwrite properties
                        existing.ContentId = contentLinksAffiliate.ContentId;
                        existing.SellerEntityId = contentLinksAffiliate.SellerEntityId;
                        existing.SellerName = contentLinksAffiliate.SellerName;
                        existing.Price = contentLinksAffiliate.Price;
                        existing.Link = contentLinksAffiliate.Link;

                        existing.Guid = contentLinksAffiliate.Guid;
                        existing.Identity = contentLinksAffiliate.Identity;
                        existing.Status = contentLinksAffiliate.Status;
                        existing.Tenant = contentLinksAffiliate.Tenant;
                        existing.CreatedBy = contentLinksAffiliate.CreatedBy;
                        existing.CreationDate = contentLinksAffiliate.CreationDate;
                        existing.ModifiedBy = contentLinksAffiliate.ModifiedBy;
                        existing.ModifiedDate = contentLinksAffiliate.ModifiedDate;

                        // EF will automatically track "existing" and update it.
                    }

                    int affected = await _context.SaveChangesAsync();

                    records.Add(new Record
                    {
                        id = contentLinksAffiliate.Id,
                        mode = isNew ? "inserted" : "updated",
                        content_id = contentLinksAffiliate.ContentId,
                        status = 0,
                        message = isNew ? affected + " Record inserted successfully" : affected + "Record updated successfully"
                    });
                }

                return Ok(new AffiliateUpsertResponse
                {
                    Records = records,
                    Status = 0,
                    Message = "",
                    ContentId = 0
                });
            }
            catch (Exception ex)
            {
                // automatically triggers jQuery error/fail
                return StatusCode(500, new { status = "error", message = ex.Message });
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveImageSingleLink(ContentLinksMedia contentLinksMedia)
        {
            try
            {
                bool isNew = false;
                int affected = 0;

                if (contentLinksMedia == null)
                    return BadRequest(new { status = "error", message = "No data received" });

                // Check if record exists (based on PK Id)
                var existing = await _context.ContentLinksMedia
                                             .FirstOrDefaultAsync(x => x.ContentId == contentLinksMedia.ContentId &&
                                             x.MediaTypeId == contentLinksMedia.MediaTypeId &&
                                             x.Link == contentLinksMedia.Link);

                if (string.IsNullOrEmpty(contentLinksMedia.Link))
                {
                    return Ok(new
                    {
                        id = contentLinksMedia.Id,
                        status = "skipped",
                        content_id = contentLinksMedia.ContentId,
                        status_code = 200,
                        message = "incomplete data received"
                    });
                }
                if (existing == null)
                {
                    // INSERT
                    _context.ContentLinksMedia.Add(contentLinksMedia);
                    isNew = true;
                }
                else
                {
                    // UPDATE – overwrite properties
                    existing.ContentId = contentLinksMedia.ContentId;
                    existing.MediaTypeId = contentLinksMedia.MediaTypeId;
                    existing.Link = contentLinksMedia.Link;

                    existing.Guid = contentLinksMedia.Guid;
                    existing.Identity = contentLinksMedia.Identity;
                    existing.Status = contentLinksMedia.Status;
                    existing.Tenant = contentLinksMedia.Tenant;
                    existing.CreatedBy = contentLinksMedia.CreatedBy;
                    existing.CreationDate = contentLinksMedia.CreationDate;
                    existing.ModifiedBy = contentLinksMedia.ModifiedBy;
                    existing.ModifiedDate = contentLinksMedia.ModifiedDate;

                    // EF will automatically track "existing" and update it.
                }

                affected = await _context.SaveChangesAsync();

                return Ok(new
                {
                    id = contentLinksMedia.Id,
                    status = isNew ? "inserted" : "updated",
                    content_id = contentLinksMedia.ContentId,
                    status_code = 200,
                    message = isNew ? affected + " Record inserted successfully" : affected + " Record updated successfully"
                });
            }
            catch (Exception ex)
            {
                // automatically triggers jQuery error/fail
                return StatusCode(500, new { status = "error", message = ex.Message });
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<int> SaveImagesData(List<ContentLinksMedia> contentLinksMedias)
        {
            try
            {
                if (contentLinksMedias == null)
                    return 0;

                foreach (ContentLinksMedia contentLinksMedia in contentLinksMedias)
                {
                    // Check if record exists (based on PK Id)
                    var existing = await _context.ContentLinksMedia
                                                 .FirstOrDefaultAsync(x => x.ContentId == contentLinksMedia.ContentId &&
                                                 x.MediaTypeId == contentLinksMedia.MediaTypeId &&
                                                 x.Link == contentLinksMedia.Link);

                    if (string.IsNullOrEmpty(contentLinksMedia.Link))
                    {
                        continue;
                    }
                    if (existing == null)
                    {
                        // INSERT
                        _context.ContentLinksMedia.Add(contentLinksMedia);
                    }
                    else
                    {
                        // UPDATE – overwrite properties
                        existing.ContentId = contentLinksMedia.ContentId;
                        existing.MediaTypeId = contentLinksMedia.MediaTypeId;
                        existing.Link = contentLinksMedia.Link;

                        existing.Guid = contentLinksMedia.Guid;
                        existing.Identity = contentLinksMedia.Identity;
                        existing.Status = contentLinksMedia.Status;
                        existing.Tenant = contentLinksMedia.Tenant;
                        existing.CreatedBy = contentLinksMedia.CreatedBy;
                        existing.CreationDate = contentLinksMedia.CreationDate;
                        existing.ModifiedBy = contentLinksMedia.ModifiedBy;
                        existing.ModifiedDate = contentLinksMedia.ModifiedDate;

                        // EF will automatically track "existing" and update it.
                    }

                    await _context.SaveChangesAsync();
                }
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveVideoSingleLink(ContentLinksMedia contentLinksMedia)
        {
            try
            {
                bool isNew = false;
                int affected = 0;

                if (contentLinksMedia == null)
                    return BadRequest(new { status = "error", message = "No data received" });

                if (string.IsNullOrEmpty(contentLinksMedia.Link))
                {
                    return Ok(new
                    {
                        id = contentLinksMedia.Id,
                        status = "skipped",
                        content_id = contentLinksMedia.ContentId,
                        status_code = 200,
                        message = "incomplete data received"
                    });
                }
                // Check if record exists (based on PK Id)
                var existing = await _context.ContentLinksMedia
                                                .FirstOrDefaultAsync(x => x.ContentId == contentLinksMedia.ContentId &&
                                                x.MediaTypeId == contentLinksMedia.MediaTypeId &&
                                                x.Link == contentLinksMedia.Link);
                if (existing == null)
                {
                    // INSERT
                    _context.ContentLinksMedia.Add(contentLinksMedia);
                    isNew = true;
                }
                else
                {
                    // UPDATE – overwrite properties
                    existing.ContentId = contentLinksMedia.ContentId;
                    existing.MediaTypeId = contentLinksMedia.MediaTypeId;
                    existing.Link = contentLinksMedia.Link;

                    existing.Guid = contentLinksMedia.Guid;
                    existing.Identity = contentLinksMedia.Identity;
                    existing.Status = contentLinksMedia.Status;
                    existing.Tenant = contentLinksMedia.Tenant;
                    existing.CreatedBy = contentLinksMedia.CreatedBy;
                    existing.CreationDate = contentLinksMedia.CreationDate;
                    existing.ModifiedBy = contentLinksMedia.ModifiedBy;
                    existing.ModifiedDate = contentLinksMedia.ModifiedDate;

                    // EF will automatically track "existing" and update it.
                }

                affected = await _context.SaveChangesAsync();

                return Ok(new
                {
                    id = contentLinksMedia.Id,
                    status = isNew ? "inserted" : "updated",
                    content_id = contentLinksMedia.ContentId,
                    status_code = 200,
                    message = isNew ? affected + " Record inserted successfully" : affected + " Record updated successfully"
                });
            }
            catch (Exception ex)
            {
                // automatically triggers jQuery error/fail
                return StatusCode(500, new { status = "error", message = ex.Message });
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<int> SaveVideosData(List<ContentLinksMedia> contentLinksMedias)
        {
            try
            {
                if (contentLinksMedias == null)
                    return 0;

                foreach (ContentLinksMedia contentLinksMedia in contentLinksMedias)
                {
                    if (string.IsNullOrEmpty(contentLinksMedia.Link))
                    {
                        continue;
                    }
                    // Check if record exists (based on PK Id)
                    var existing = await _context.ContentLinksMedia
                                                 .FirstOrDefaultAsync(x => x.ContentId == contentLinksMedia.ContentId &&
                                                 x.MediaTypeId == contentLinksMedia.MediaTypeId &&
                                                 x.Link == contentLinksMedia.Link);
                    if (existing == null)
                    {
                        // INSERT
                        _context.ContentLinksMedia.Add(contentLinksMedia);
                    }
                    else
                    {
                        // UPDATE – overwrite properties
                        existing.ContentId = contentLinksMedia.ContentId;
                        existing.MediaTypeId = contentLinksMedia.MediaTypeId;
                        existing.Link = contentLinksMedia.Link;

                        existing.Guid = contentLinksMedia.Guid;
                        existing.Identity = contentLinksMedia.Identity;
                        existing.Status = contentLinksMedia.Status;
                        existing.Tenant = contentLinksMedia.Tenant;
                        existing.CreatedBy = contentLinksMedia.CreatedBy;
                        existing.CreationDate = contentLinksMedia.CreationDate;
                        existing.ModifiedBy = contentLinksMedia.ModifiedBy;
                        existing.ModifiedDate = contentLinksMedia.ModifiedDate;

                        // EF will automatically track "existing" and update it.
                    }

                    await _context.SaveChangesAsync();
                }
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveSocialSingleLink(ContentLinksSocial contentLinksSocial)
        {
            try
            {
                bool isNew = false;
                int affected = 0;

                if (contentLinksSocial == null)
                    return BadRequest(new { status = "error", message = "No data received" });

                if (string.IsNullOrEmpty(contentLinksSocial.Link))
                {
                    return Ok(new
                    {
                        id = contentLinksSocial.Id,
                        status = "skipped",
                        content_id = contentLinksSocial.ContentId,
                        status_code = 200,
                        message = "incomplete data received"
                    });
                }
                // Check if record exists (based on PK Id)
                var existing = await _context.ContentLinksSocial
                                             .FirstOrDefaultAsync(x => x.ContentId == contentLinksSocial.ContentId &&
                                             x.SocialMediaTypeId == contentLinksSocial.SocialMediaTypeId &&
                                             x.Link == contentLinksSocial.Link);
                if (existing == null)
                {
                    // INSERT
                    _context.ContentLinksSocial.Add(contentLinksSocial);
                    isNew = true;
                }
                else
                {
                    // UPDATE – overwrite properties
                    existing.ContentId = contentLinksSocial.ContentId;
                    existing.SocialMediaTypeId = contentLinksSocial.SocialMediaTypeId;
                    existing.Link = contentLinksSocial.Link;

                    existing.Guid = contentLinksSocial.Guid;
                    existing.Identity = contentLinksSocial.Identity;
                    existing.Status = contentLinksSocial.Status;
                    existing.Tenant = contentLinksSocial.Tenant;
                    existing.CreatedBy = contentLinksSocial.CreatedBy;
                    existing.CreationDate = contentLinksSocial.CreationDate;
                    existing.ModifiedBy = contentLinksSocial.ModifiedBy;
                    existing.ModifiedDate = contentLinksSocial.ModifiedDate;

                    // EF will automatically track "existing" and update it.
                }

                affected = await _context.SaveChangesAsync();

                return Ok(new
                {
                    id = contentLinksSocial.Id,
                    status = isNew ? "inserted" : "updated",
                    content_id = contentLinksSocial.ContentId,
                    status_code = 200,
                    message = isNew ? affected + " Record inserted successfully" : affected + " Record updated successfully"
                });
            }
            catch (Exception ex)
            {
                // automatically triggers jQuery error/fail
                return StatusCode(500, new { status = "error", message = ex.Message });
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<int> SaveSocialsData(List<ContentLinksSocial> contentLinksSocials)
        {
            try
            {
                if (contentLinksSocials == null)
                    return 0;

                foreach (ContentLinksSocial contentLinksSocial in contentLinksSocials)
                {
                    if (string.IsNullOrEmpty(contentLinksSocial.Link))
                    {
                        continue;
                    }
                    // Check if record exists (based on PK Id)
                    var existing = await _context.ContentLinksSocial
                                                 .FirstOrDefaultAsync(x => x.ContentId == contentLinksSocial.ContentId &&
                                                 x.SocialMediaTypeId == contentLinksSocial.SocialMediaTypeId &&
                                                 x.Link == contentLinksSocial.Link);
                    if (existing == null)
                    {
                        // INSERT
                        _context.ContentLinksSocial.Add(contentLinksSocial);
                    }
                    else
                    {
                        // UPDATE – overwrite properties
                        existing.ContentId = contentLinksSocial.ContentId;
                        existing.SocialMediaTypeId = contentLinksSocial.SocialMediaTypeId;
                        existing.Link = contentLinksSocial.Link;

                        existing.Guid = contentLinksSocial.Guid;
                        existing.Identity = contentLinksSocial.Identity;
                        existing.Status = contentLinksSocial.Status;
                        existing.Tenant = contentLinksSocial.Tenant;
                        existing.CreatedBy = contentLinksSocial.CreatedBy;
                        existing.CreationDate = contentLinksSocial.CreationDate;
                        existing.ModifiedBy = contentLinksSocial.ModifiedBy;
                        existing.ModifiedDate = contentLinksSocial.ModifiedDate;

                        // EF will automatically track "existing" and update it.
                    }

                    await _context.SaveChangesAsync();
                }
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveArticleSingleData(ContentLinksReference contentLinksReference)
        {
            try
            {
                bool isNew = false;
                int affected = 0;

                if (contentLinksReference == null)
                    return BadRequest(new { status = "error", message = "No data received" });

                if (string.IsNullOrEmpty(contentLinksReference.Title) ||
                    string.IsNullOrEmpty(contentLinksReference.ImageLink) ||
                    string.IsNullOrEmpty(contentLinksReference.Link))
                {
                    return Ok(new
                    {
                        id = contentLinksReference.Id,
                        status = "skipped",
                        content_id = contentLinksReference.ContentId,
                        status_code = 200,
                        message = "incomplete data received"
                    });
                }
                // Check if record exists (based on PK Id)
                var existing = await _context.ContentLinksReference
                                             .FirstOrDefaultAsync(x => x.ContentId == contentLinksReference.ContentId &&
                                             x.ContentId == contentLinksReference.ContentId &&
                                             x.ReferenceTypeId == contentLinksReference.ReferenceTypeId &&
                                             x.Title == contentLinksReference.Title &&
                                             x.ImageLink == contentLinksReference.ImageLink &&
                                             x.Link == contentLinksReference.Link);
                if (existing == null)
                {
                    // INSERT
                    _context.ContentLinksReference.Add(contentLinksReference);
                    isNew = true;
                }
                else
                {
                    // UPDATE – overwrite properties
                    existing.ContentId = contentLinksReference.ContentId;
                    existing.ReferenceTypeId = contentLinksReference.ReferenceTypeId;
                    existing.Title = contentLinksReference.Title;
                    existing.ImageLink = contentLinksReference.ImageLink;
                    existing.Link = contentLinksReference.Link;

                    existing.Guid = contentLinksReference.Guid;
                    existing.Identity = contentLinksReference.Identity;
                    existing.Status = contentLinksReference.Status;
                    existing.Tenant = contentLinksReference.Tenant;
                    existing.CreatedBy = contentLinksReference.CreatedBy;
                    existing.CreationDate = contentLinksReference.CreationDate;
                    existing.ModifiedBy = contentLinksReference.ModifiedBy;
                    existing.ModifiedDate = contentLinksReference.ModifiedDate;

                    // EF will automatically track "existing" and update it.
                }

                affected = await _context.SaveChangesAsync();

                return Ok(new
                {
                    id = contentLinksReference.Id,
                    status = isNew ? "inserted" : "updated",
                    content_id = contentLinksReference.ContentId,
                    status_code = 200,
                    message = isNew ? affected + " Record inserted successfully" : affected + " Record updated successfully"
                });
            }
            catch (Exception ex)
            {
                // automatically triggers jQuery error/fail
                return StatusCode(500, new { status = "error", message = ex.Message });
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<int> SaveArticleData(List<ContentLinksReference> contentLinksReferences)
        {
            try
            {
                if (contentLinksReferences == null)
                    return 0;

                foreach (ContentLinksReference contentLinksReference in contentLinksReferences)
                {
                    if (string.IsNullOrEmpty(contentLinksReference.Title) ||
                        string.IsNullOrEmpty(contentLinksReference.ImageLink) ||
                        string.IsNullOrEmpty(contentLinksReference.Link))
                    {
                        continue;
                    }
                    // Check if record exists (based on PK Id)
                    var existing = await _context.ContentLinksReference
                                                 .FirstOrDefaultAsync(x => x.ContentId == contentLinksReference.ContentId &&
                                                 x.ReferenceTypeId == contentLinksReference.ReferenceTypeId &&
                                                 x.Title == contentLinksReference.Title &&
                                                 x.ImageLink == contentLinksReference.ImageLink &&
                                                 x.Link == contentLinksReference.Link);
                    if (existing == null)
                    {
                        // INSERT
                        _context.ContentLinksReference.Add(contentLinksReference);
                    }
                    else
                    {
                        // UPDATE – overwrite properties
                        existing.ContentId = contentLinksReference.ContentId;
                        existing.ReferenceTypeId = contentLinksReference.ReferenceTypeId;
                        existing.Title = contentLinksReference.Title;
                        existing.ImageLink = contentLinksReference.ImageLink;
                        existing.Link = contentLinksReference.Link;

                        existing.Guid = contentLinksReference.Guid;
                        existing.Identity = contentLinksReference.Identity;
                        existing.Status = contentLinksReference.Status;
                        existing.Tenant = contentLinksReference.Tenant;
                        existing.CreatedBy = contentLinksReference.CreatedBy;
                        existing.CreationDate = contentLinksReference.CreationDate;
                        existing.ModifiedBy = contentLinksReference.ModifiedBy;
                        existing.ModifiedDate = contentLinksReference.ModifiedDate;

                        // EF will automatically track "existing" and update it.
                    }

                    await _context.SaveChangesAsync();
                }
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> SavePostSingleData(ContentLinksReference contentLinksReference)
        {
            try
            {
                bool isNew = false;
                int affected = 0;

                if (contentLinksReference == null)
                    return BadRequest(new { status = "error", message = "No data received" });

                if (string.IsNullOrEmpty(contentLinksReference.Title) ||
                    string.IsNullOrEmpty(contentLinksReference.ImageLink) ||
                    string.IsNullOrEmpty(contentLinksReference.Link))
                {
                    return Ok(new
                    {
                        id = contentLinksReference.Id,
                        status = "skipped",
                        content_id = contentLinksReference.ContentId,
                        status_code = 200,
                        message = "incomplete data received"
                    });
                }
                // Check if record exists (based on PK Id)
                var existing = await _context.ContentLinksReference
                                             .FirstOrDefaultAsync(x => x.ContentId == contentLinksReference.ContentId &&
                                             x.ContentId == contentLinksReference.ContentId &&
                                             x.ReferenceTypeId == contentLinksReference.ReferenceTypeId &&
                                             x.Title == contentLinksReference.Title &&
                                             x.ImageLink == contentLinksReference.ImageLink &&
                                             x.Link == contentLinksReference.Link);
                if (existing == null)
                {
                    // INSERT
                    _context.ContentLinksReference.Add(contentLinksReference);
                    isNew = true;
                }
                else
                {
                    // UPDATE – overwrite properties
                    existing.ContentId = contentLinksReference.ContentId;
                    existing.ReferenceTypeId = contentLinksReference.ReferenceTypeId;
                    existing.Title = contentLinksReference.Title;
                    existing.ImageLink = contentLinksReference.ImageLink;
                    existing.Link = contentLinksReference.Link;

                    existing.Guid = contentLinksReference.Guid;
                    existing.Identity = contentLinksReference.Identity;
                    existing.Status = contentLinksReference.Status;
                    existing.Tenant = contentLinksReference.Tenant;
                    existing.CreatedBy = contentLinksReference.CreatedBy;
                    existing.CreationDate = contentLinksReference.CreationDate;
                    existing.ModifiedBy = contentLinksReference.ModifiedBy;
                    existing.ModifiedDate = contentLinksReference.ModifiedDate;

                    // EF will automatically track "existing" and update it.
                }

                affected = await _context.SaveChangesAsync();

                return Ok(new
                {
                    id = contentLinksReference.Id,
                    status = isNew ? "inserted" : "updated",
                    content_id = contentLinksReference.ContentId,
                    status_code = 200,
                    message = isNew ? affected + " Record inserted successfully" : affected + " Record updated successfully"
                });
            }
            catch (Exception ex)
            {
                // automatically triggers jQuery error/fail
                return StatusCode(500, new { status = "error", message = ex.Message });
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<int> SavePostData(List<ContentLinksReference> contentLinksReferences)
        {
            try
            {
                if (contentLinksReferences == null)
                    return 0;

                foreach (ContentLinksReference contentLinksReference in contentLinksReferences)
                {
                    if (string.IsNullOrEmpty(contentLinksReference.Title) ||
                        string.IsNullOrEmpty(contentLinksReference.ImageLink) ||
                        string.IsNullOrEmpty(contentLinksReference.Link))
                    {
                        continue;
                    }
                    // Check if record exists (based on PK Id)
                    var existing = await _context.ContentLinksReference
                                                 .FirstOrDefaultAsync(x => x.ContentId == contentLinksReference.ContentId &&
                                                 x.ReferenceTypeId == contentLinksReference.ReferenceTypeId &&
                                                 x.Title == contentLinksReference.Title &&
                                                 x.ImageLink == contentLinksReference.ImageLink &&
                                                 x.Link == contentLinksReference.Link);
                    if (existing == null)
                    {
                        // INSERT
                        _context.ContentLinksReference.Add(contentLinksReference);
                    }
                    else
                    {
                        // UPDATE – overwrite properties
                        existing.ContentId = contentLinksReference.ContentId;
                        existing.ReferenceTypeId = contentLinksReference.ReferenceTypeId;
                        existing.Title = contentLinksReference.Title;
                        existing.ImageLink = contentLinksReference.ImageLink;
                        existing.Link = contentLinksReference.Link;

                        existing.Guid = contentLinksReference.Guid;
                        existing.Identity = contentLinksReference.Identity;
                        existing.Status = contentLinksReference.Status;
                        existing.Tenant = contentLinksReference.Tenant;
                        existing.CreatedBy = contentLinksReference.CreatedBy;
                        existing.CreationDate = contentLinksReference.CreationDate;
                        existing.ModifiedBy = contentLinksReference.ModifiedBy;
                        existing.ModifiedDate = contentLinksReference.ModifiedDate;

                        // EF will automatically track "existing" and update it.
                    }

                    await _context.SaveChangesAsync();
                }
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DMMS.Models.ViewModels.Publisher>?> GetPublishers(int websiteId)
        {
            try
            {
                WebsiteConfig? websiteConfig = _context.WebsiteConfig.Where(x => x.Id == websiteId).FirstOrDefault();
                if (websiteConfig != null)
                {
                    DataTable dtPublishers = await DatabaseHelper.RunMySqlQueryAsync(websiteConfig.ConnectionString, "SELECT * FROM wp_users");
                    List<DMMS.Models.ViewModels.Publisher>? publishers = new List<DMMS.Models.ViewModels.Publisher>();
                    foreach (DataRow row in dtPublishers.Rows)
                    {
                        publishers.Add(new DMMS.Models.ViewModels.Publisher
                        {
                            Id = Convert.ToInt32(row["ID"]),
                            Name = row["display_name"].ToString()
                        });
                    }
                    return publishers;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveExternalSingleLinkData(ContentLinksExternalLink contentLinksExternalLink)
        {
            try
            {
                bool isNew = false;
                int affected = 0;

                if (contentLinksExternalLink == null)
                    return BadRequest(new { status = "error", message = "No data received" });

                if (string.IsNullOrEmpty(contentLinksExternalLink.WordOrSentense) || string.IsNullOrEmpty(contentLinksExternalLink.Link))
                {
                    return Ok(new
                    {
                        id = contentLinksExternalLink.Id,
                        status = "skipped",
                        content_id = contentLinksExternalLink.ContentId,
                        status_code = 200,
                        message = "incomplete data received"
                    });
                }
                // Check if record exists (based on PK Id)
                var existing = await _context.ContentLinksExternalLinks
                                             .FirstOrDefaultAsync(x => x.ContentId == contentLinksExternalLink.ContentId &&
                                             x.WordOrSentense == contentLinksExternalLink.WordOrSentense &&
                                             x.Link == contentLinksExternalLink.Link);
                if (existing == null)
                {
                    // INSERT
                    _context.ContentLinksExternalLinks.Add(contentLinksExternalLink);
                }
                else
                {
                    // UPDATE – overwrite properties
                    existing.ContentId = contentLinksExternalLink.ContentId;
                    existing.WordOrSentense = contentLinksExternalLink.WordOrSentense;
                    existing.Link = contentLinksExternalLink.Link;

                    existing.Guid = contentLinksExternalLink.Guid;
                    existing.Identity = contentLinksExternalLink.Identity;
                    existing.Status = contentLinksExternalLink.Status;
                    existing.Tenant = contentLinksExternalLink.Tenant;
                    existing.CreatedBy = contentLinksExternalLink.CreatedBy;
                    existing.CreationDate = contentLinksExternalLink.CreationDate;
                    existing.ModifiedBy = contentLinksExternalLink.ModifiedBy;
                    existing.ModifiedDate = contentLinksExternalLink.ModifiedDate;

                    // EF will automatically track "existing" and update it.
                }

                affected = await _context.SaveChangesAsync();

                return Ok(new
                {
                    id = contentLinksExternalLink.Id,
                    status = isNew ? "inserted" : "updated",
                    content_id = contentLinksExternalLink.ContentId,
                    status_code = 200,
                    message = isNew ? affected + " Record inserted successfully" : affected + " Record updated successfully"
                });
            }
            catch (Exception ex)
            {
                // automatically triggers jQuery error/fail
                return StatusCode(500, new { status = "error", message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<int> SaveExternalLinksData(List<ContentLinksExternalLink> contentLinksExternalLinks)
        {
            try
            {
                if (contentLinksExternalLinks == null)
                    return 0;

                foreach (ContentLinksExternalLink contentLinksExternalLink in contentLinksExternalLinks)
                {
                    if (string.IsNullOrEmpty(contentLinksExternalLink.WordOrSentense) || string.IsNullOrEmpty(contentLinksExternalLink.Link))
                    {
                        continue;
                    }
                    // Check if record exists (based on PK Id)
                    var existing = await _context.ContentLinksExternalLinks
                                                 .FirstOrDefaultAsync(x => x.ContentId == contentLinksExternalLink.ContentId &&
                                                 x.WordOrSentense == contentLinksExternalLink.WordOrSentense &&
                                                 x.Link == contentLinksExternalLink.Link);
                    if (existing == null)
                    {
                        // INSERT
                        _context.ContentLinksExternalLinks.Add(contentLinksExternalLink);
                    }
                    else
                    {
                        // UPDATE – overwrite properties
                        existing.ContentId = contentLinksExternalLink.ContentId;
                        existing.WordOrSentense = contentLinksExternalLink.WordOrSentense;
                        existing.Link = contentLinksExternalLink.Link;

                        existing.Guid = contentLinksExternalLink.Guid;
                        existing.Identity = contentLinksExternalLink.Identity;
                        existing.Status = contentLinksExternalLink.Status;
                        existing.Tenant = contentLinksExternalLink.Tenant;
                        existing.CreatedBy = contentLinksExternalLink.CreatedBy;
                        existing.CreationDate = contentLinksExternalLink.CreationDate;
                        existing.ModifiedBy = contentLinksExternalLink.ModifiedBy;
                        existing.ModifiedDate = contentLinksExternalLink.ModifiedDate;

                        // EF will automatically track "existing" and update it.
                    }

                    await _context.SaveChangesAsync();
                }
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAffiliateLinks(int contentId)
        {
            //var sellers = await _context.SellerEntity
            //    .Select(x => new {
            //        x.Id,
            //        x.Name,
            //    })
            //    .ToListAsync();

            //var links = await _context.ContentLinksAffiliate
            //    .Where(x => x.ContentId == contentId)
            //    .Select(x => new {
            //        x.SellerName,
            //        x.Price,
            //        x.Link
            //    })
            //    .ToListAsync();

            var links = await _context.ContentLinksAffiliate
            .Where(a => a.ContentId == contentId)
            .Join(
                _context.SellerEntity,
                a => a.SellerEntityId,
                s => s.Id,
                (a, s) => new
                {
                    SellerName = s.Name,
                    a.Price,
                    a.Link,
                    s.BackgroundColor,
                    s.TextColor,
                    s.BorderColor,
                    s.Style,
                    s.Class
                }
            )
            .ToListAsync();

            return Ok(links);
        }

        [HttpPost]
        public async Task<long>? InsertWordPressPost([FromBody] WordPressPostRequest wordPressPostRequest)
        {
            try
            {
                WebsiteConfig? websiteConfig = _context.WebsiteConfig.Where(x => x.Id == wordPressPostRequest.WebsiteId).FirstOrDefault();
                if (websiteConfig != null)
                {
                    using var conn = new MySqlConnection(websiteConfig.ConnectionString);
                    await conn.OpenAsync();

                    string insertPostSql = @"
                    INSERT INTO wp_posts
                    (
                        post_author,
                        post_date,
                        post_date_gmt,
                        post_content,
                        post_title,
                        post_excerpt,
                        post_status,
                        comment_status,
                        ping_status,
                        post_name,
                        post_modified,
                        post_modified_gmt,
                        post_type
                    )
                    VALUES
                    (
                        1,
                        NOW(),
                        UTC_TIMESTAMP(),
                        @content,
                        @title,
                        '',
                        'publish',
                        'open',
                        'open',
                        @slug,
                        NOW(),
                        UTC_TIMESTAMP(),
                        'post'
                    );

                    SELECT LAST_INSERT_ID();
                    ";

                    string slug = wordPressPostRequest.Title.ToLower().Replace(" ", "-");

                    using var cmd = new MySqlCommand(insertPostSql, conn);
                    cmd.Parameters.AddWithValue("@title", wordPressPostRequest.Title);
                    cmd.Parameters.AddWithValue("@slug", slug);
                    cmd.Parameters.AddWithValue("@content", wordPressPostRequest.Content);

                    object? result = await cmd.ExecuteScalarAsync();
                    long postId = Convert.ToInt64(result);

                    await InsertMeta(conn, postId, "string key", "string value");

                    return postId;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task InsertMeta(MySqlConnection? conn, long postId, string key, string value)
        {
            string sql = @"INSERT INTO wp_postmeta (post_id, meta_key, meta_value)
                   VALUES (@id, @key, @value)";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", postId);
            cmd.Parameters.AddWithValue("@key", key);
            cmd.Parameters.AddWithValue("@value", value);
            await cmd.ExecuteNonQueryAsync();
        }

        private readonly string _wpUrl = "https://yourwebsite.com/wp-json/wp/v2/posts";
        private readonly string _username = "women.smarterdeals@gmail.com";
        private readonly string _appPassword = "VCSD@20200"; // application password

        [HttpPost]
        public async Task<long?> CreatePostAsync([FromBody] WordPressPostRequest wordPressPostRequest)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    // WordPress authentication (Basic Auth)
                    var authToken = Convert.ToBase64String(
                        Encoding.ASCII.GetBytes($"{_username}:{_appPassword}")
                    );
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Basic", authToken);

                    // Prepare payload
                    var postData = new
                    {
                        title = wordPressPostRequest.Title,
                        content = wordPressPostRequest.Content,
                        Status = "publish",    // or "draft"
                        meta = new Dictionary<string, object>()
                        {
                            { "website_id", wordPressPostRequest.WebsiteId } // your custom field if needed
                        }
                    };

                    string jsonData = JsonConvert.SerializeObject(postData);

                    var response = await client.PostAsync(
                        _wpUrl,
                        new StringContent(jsonData, Encoding.UTF8, "application/json")
                    );

                    string result = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception($"Error: {result}");
                    }

                    // Deserialize result to get Post ID
                    dynamic? json = JsonConvert.DeserializeObject(result);
                    long? postId = json.id;

                    return postId;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error posting to WordPress: " + ex.Message);
                return null;
            }
        }

        //By API
        [HttpPost]
        public async Task<string> CreateWordPressPost([FromBody] PostRequest req)
        {
            string wpApiUrl = req.WebsiteUrl + "/wp-json/wp/v2/posts";
            string username = req.Username;
            string appPassword = req.ApplicationPassword;

            var authToken = Convert.ToBase64String(
                Encoding.ASCII.GetBytes($"{username}:{appPassword}")
            );

            using var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authToken);

            var postData = new
            {
                title = req.Title,
                content = req.Content,
                status = "publish",
                categories = new int[] { req.CategoryId },
                tags = req.TagIds,
                meta = new Dictionary<string, string>
                {
                    { "_yoast_wpseo_title", req.SeoTitle },
                    { "_yoast_wpseo_metadesc", req.SeoDescription }
                }
            };

            var json = JsonConvert.SerializeObject(postData);
            var response = await client.PostAsync(wpApiUrl,
                new StringContent(json, Encoding.UTF8, "application/json"));

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<int> UploadFeaturedImage(string siteUrl, string username, string appPass, byte[] imageBytes, string fileName)
        {
            var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{appPass}"));
            using var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", auth);

            var content = new ByteArrayContent(imageBytes);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
            content.Headers.Add("Content-Disposition", $"attachment; filename={fileName}");

            var response = await client.PostAsync($"{siteUrl}/wp-json/wp/v2/media", content);
            var json = await response.Content.ReadAsStringAsync();

            dynamic data = JsonConvert.DeserializeObject(json);
            return data.id;
        }
    }
}