namespace DMMS.Models.BaseIdentity;
using DMMS.Models;

public class BaseUser : Base.Base
{
    //public required Company Company { get; set; }
    public int CompanyId { get; set; }
    //public required User User { get; set; }
    public int UserId { get; set; }
}