using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMMS.Models.Base;

public class BaseNonNamable : Base
{
    [Key]
    [Column(Order = 0)]
    public int Id { get; set; }
}