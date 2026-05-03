using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMMS.Models
{
    public class ContentLinksAffiliate : BaseIdentity.BaseUser
    {
        public required Content Content { get; set; }
        public required int ContentId { get; set; }
        public SellerEntity? SellerEntity { get; set; }
        public int? SellerEntityId { get; set; }
        public string? SellerName { get; set; }
        public required string Price { get; set; }
        public required string Link { get; set; }
    }
}