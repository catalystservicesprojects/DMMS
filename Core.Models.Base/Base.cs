using System.ComponentModel.DataAnnotations;

namespace DMMS.Models.Base;

public class Base
{
    [Key]
    public int Id { get; set; }
    public Guid? Guid { get; set; } = new Guid(); // For Unique Query String
    public string? Identity { get; set; } // Public access Identity
    public int? Status { get; set; } = 0;
    public int? Tenant { get; set; } = 0;
    public int? CreatedBy { get; set; } = 0;
    public DateTime? CreationDate { get; set; } = DateTime.Now;
    public int? ModifiedBy { get; set; } = 0;
    public DateTime? ModifiedDate { get; set; } = DateTime.Now;
}