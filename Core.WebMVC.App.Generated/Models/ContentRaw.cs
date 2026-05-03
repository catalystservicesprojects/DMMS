using System;

namespace DMMS.Models
{
    // Auto-generated flattened model for DMMS.Models.ContentRaw
    public class ContentRaw
    {
        public Content Content { get; set; }
        public int ContentId { get; set; }
        public string Value { get; set; }
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
