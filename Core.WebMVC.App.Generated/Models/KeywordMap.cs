using System;

namespace DMMS.Models
{
    // Auto-generated flattened model for DMMS.Models.KeywordMap
    public class KeywordMap
    {
        public Keyword Keyword { get; set; }
        public int KeywordId { get; set; }
        public Niche Niche { get; set; }
        public int NicheId { get; set; }
        public Tag Tag { get; set; }
        public int TagId { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public int CompanyId { get; set; }
        public int UserId { get; set; }
        public int Id { get; set; }
        public Nullable<Guid> Guid { get; set; }
        public string Identity { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> Tenant { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<DateTime> CreationDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
    }
}
