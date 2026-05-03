using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMMS.Models.BaseIdentity;

public class BaseUserType : BaseUser
{
    [Key]
    [Column(Order = 0)]
    public new int Id { get; set; }

    [Column(Order = 1)]
    public string? Name { get; set; } = "";

    [Column(Order = 2)]
    public string? Alias { get; set; } = "";

    [Column(Order = 3)]
    public required string? Value { get; set; }
}