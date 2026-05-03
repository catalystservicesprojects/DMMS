using System;

namespace DMMS.Models
{
    // Auto-generated flattened model for DMMS.Models.Company
    public class Company
    {
        public int Parent { get; set; }
        public int Order { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
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
