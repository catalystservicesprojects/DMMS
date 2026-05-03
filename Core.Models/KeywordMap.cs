using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMMS.Models
{
    public class KeywordMap : BaseIdentity.BaseUser
    {
        public required KeywordStat Keyword { get; set; }
        public required int KeywordId { get; set;}

        public required Niche Niche { get; set; }
        public required int NicheId { get; set; }

        public required Tag Tag { get; set; }
        public required int TagId { get; set; }

        public required Category Category { get; set; }
        public required int CategoryId { get; set; }
    }
}