using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMMS.Models.BaseIdentity;

public class BaseUserNonNamable : BaseUser
{
    [Key]
    [Column(Order = 0)]
    public new int Id { get; set; }
}