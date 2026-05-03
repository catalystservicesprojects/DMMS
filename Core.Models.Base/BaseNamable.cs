using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMMS.Models.Base;

public class BaseNamable : Base
{
    [Key]
    [Column(Order = 0)]
    public new int Id { get; set; }

    [Column(Order = 1)]
    public required string Name { get; set; } = "";

    [Column(Order = 2)]
    public string? Alias { get; set; } = "";
}