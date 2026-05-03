using System;

namespace DMMS.Models
{
    // Auto-generated flattened model for DMMS.Models.CMSType
    public class CMSType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Value { get; set; }
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
