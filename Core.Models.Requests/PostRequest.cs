using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMMS.Models.Requests
{
    public class PostRequest
    {
        public string WebsiteUrl { get; set; }
        public string Username { get; set; }
        public string ApplicationPassword { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }

        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }

        public int CategoryId { get; set; }
        public List<int> TagIds { get; set; }

        public int? FeaturedImageId { get; set; }
    }
}
