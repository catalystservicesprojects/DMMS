using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMMS.Models.Base;

public class BaseNamableNullable : Base
{
    [Key]
    [Column(Order = 0)]
    public int Id { get; set; }

    [Column(Order = 1)]
    public string? Name { get; set; } = "";

    [Column(Order = 2)]
    public string? Alias { get; set; } = "";
}