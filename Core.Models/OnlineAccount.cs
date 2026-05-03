using DMMS.Models.Base;
using DMMS.Models.BaseIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DMMS.Utility.Enumerations.OnlineAccountEnum;

namespace DMMS.Models
{
    public class OnlineAccount : BaseNamable
    {
        public OnlineAccountTypes OnlineAccountType { get; set; }
        public required string Link { get; set; }
        public required string Icon { get; set; } = "";
    }
}