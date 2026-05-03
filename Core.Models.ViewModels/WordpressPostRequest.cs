using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMMS.Models.ViewModels
{
    public class WordPressPostRequest
    {
        public int WebsiteId { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
    }
}
